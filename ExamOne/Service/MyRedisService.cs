using ExamOne.Entity;
using ExamOne.Helper;
using ExamOne.Models;
using MongoDB.Driver;
using StackExchange.Redis;
using System.Text.Json;

namespace ExamOne.Service
{
    public class MyRedisService : BackgroundService
    {
        private readonly ILogger<MyRedisService> _logger;
        private ExamOneMongoDBContext _examOneMongoDBContext;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        public MyRedisService(ILogger<MyRedisService> logger, ExamOneMongoDBContext examOneMongoDBContext
            , IConnectionMultiplexer redis)
        {
            _logger = logger;
            _examOneMongoDBContext = examOneMongoDBContext;
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MyRedisService started at: {time}", DateTime.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                var startTime = DateTime.Now;
                try
                {
                    _logger.LogInformation("Task started at: {time}", startTime);

                    await MarkExamAsync(stoppingToken);

                    _logger.LogInformation("Task finished at: {time}", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in MyRedisService");
                }

                _logger.LogInformation("Waiting 1 minutes before next run...");
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    _logger.LogError("Task canceled, stopping service...");
                }
            }

            _logger.LogInformation("MyRedisService stopped at: {time}", DateTime.Now);
        }

        private async Task MarkExamAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Checking Redis connection at: {time}", DateTime.Now);
                TimeSpan max = TimeSpan.FromMinutes(20);
                //return;
                var examHistories = await _examOneMongoDBContext.ExamHistories.Find(c => string.IsNullOrEmpty(c.SelectedAnswers) && c.RetryCount < 3).ToListAsync();
                foreach (var item in examHistories)
                {
                    var redisResult = await _db.StringGetAsync($"exam:{item.Id}");
                    var redisResult2 = await _db.StringGetAsync($"time:{item.Id}");
                    var redisResult3 = await _db.StringGetAsync($"estimate:{item.CreatedBy}");
                    if (!string.IsNullOrEmpty(redisResult3))
                    {
                        var exists = await _examOneMongoDBContext.Estimates
                                    .Find(x => x.CreatedBy == item.CreatedBy)
                                    .AnyAsync();
                        if (!exists)
                        {
                            var estimate = new Estimate
                            {
                                ExamId = item.Id,
                                CreatedBy = item.CreatedBy,
                                EstimateCount = int.Parse(redisResult3.ToString()),
                                StartDate = Constant.GetDateTimeVN()
                            };
                            await _examOneMongoDBContext.Estimates.InsertOneAsync(estimate);
                            await _db.KeyDeleteAsync($"estimate:{item.Id}");
                        }

                    }
                    if (redisResult.IsNullOrEmpty || redisResult2.IsNullOrEmpty)
                    {
                        var retryCount = item.RetryCount + 1;
                        _logger.LogError($"ID {item.Id}: not found. Retry: {retryCount}");
                        var update2 = Builders<ExamHistory>.Update
                                    .Set(x => x.RetryCount, retryCount);
                        await _examOneMongoDBContext.ExamHistories.UpdateOneAsync(c => c.Id == item.Id, update2);
                        continue;
                    }
                    var answers = JsonSerializer.Deserialize<List<AnswerModel>>(redisResult.ToString() ?? "[]") ?? new List<AnswerModel>();
                    var questions = JsonSerializer.Deserialize<List<QuestionBank>>(item.Items ?? "[]") ?? new List<QuestionBank>();
                    var correctAnswer = 0;
                    foreach (var question in questions)
                    {
                        var questionItems = JsonSerializer.Deserialize<List<OptionModel>>(question.Items ?? "[]") ?? new List<OptionModel>();
                        var correctItem = questionItems.FirstOrDefault(c => c.IsCorrect);
                        if (correctItem == null)
                        {
                            _logger.LogError($"ID {item.Id}: correct answer not found");
                            continue;
                        }
                        correctAnswer += answers.FirstOrDefault(x => x.id == question.Id && x.answerId == correctItem.Id) != null ? 1 : 0;
                    }

                    var completeDurations = DateTime.Parse(redisResult2.ToString()) - Constant.GetDateTimeFromMongo(item.StartDate);
                    if (completeDurations > max) completeDurations = max;
                    var update = Builders<ExamHistory>.Update
                        .Set(x => x.SelectedAnswers, redisResult.ToString())
                        .Set(x => x.TotalCorrectAnswers, correctAnswer)
                        .Set(x => x.EndDate, DateTime.Parse(redisResult2.ToString()))
                        .Set(x => x.MarkDate, Constant.GetDateTimeVN())
                        .Set(x => x.ComplatedDuration,completeDurations.TotalMilliseconds);
                    await _examOneMongoDBContext.ExamHistories.UpdateOneAsync(c => c.Id == item.Id, update);
                    await _db.KeyDeleteAsync($"exam:{item.Id}");
                    await _db.KeyDeleteAsync($"time:{item.Id}");
                    await _db.KeyDeleteAsync($"questions:{item.Id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

        }
    }
}
