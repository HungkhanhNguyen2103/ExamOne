using ExamOne.Entity;
using ExamOne.Helper;
using ExamOne.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using StackExchange.Redis;
using System.Text.Json;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace ExamOne.Service
{
    public interface IExamService
    {
        Task<ResponderData<ExamModel>> GetIntructionExam(string createBy);
        Task<ResponderData<string>> SettingExam(ExamModel model);
        Task<ResponderData<string>> CompleteExam(ExamAnswerModel model);
        ResponderData<ResultExamModel> ResultExam(ExamModel model,string selectedAnswers);
        Task<ResponderData<string>> CreateExamTest(string createBy,string branchCode);
        Task<ResponderData<ExamModel>> GetExamTest(string id,string createBy);
        Task<ResponderData<ExamProgressModel>> GetExamDetail(string id);
        Task<ResponderData<ExamProgressModel>> GetListExamHistory();
        Task<ResponderData<string>> RemoveExamHistory(string id);
        Task<ResponderData<string>> GetExamData(string key);
        Task<ResponderData<string>> SetExamData(string key, string data);
        Task<ResponderData<string>> AddData();
        //Task<ResponderData<string>> GetListExamResult(int status);
    }
    public class ExamService : IExamService
    {
        private ExamOneDbContext _examOneDbContext;
        private ExamOneMongoDBContext _examOneMongoDBContext;
        private readonly UserManager<Account> _userManager;
        private readonly IConnectionMultiplexer _redis;
        private readonly IWebHostEnvironment _env;
        private readonly IDatabase _db;
        public ExamService(ExamOneDbContext examOneDbContext, ExamOneMongoDBContext examOneMongoDBContext
            ,IConnectionMultiplexer redis, UserManager<Account> userManager, IWebHostEnvironment env)
        {
            _examOneDbContext = examOneDbContext;
            _examOneMongoDBContext = examOneMongoDBContext;
            _redis = redis;
            _userManager = userManager;
            _env = env;
            _db = _redis.GetDatabase();
        }

        public async Task<ResponderData<string>> CompleteExam(ExamAnswerModel model)
        {
            var now = DateTime.Now.ToString();
            var result = new ResponderData<string>();
            if(model == null || string.IsNullOrEmpty(model.Id) || string.IsNullOrEmpty(model.ExamAnswer))
            {
                result.Message = "Dữ liệu không hợp lệ";
                return result;
            }

            var examHistory = await _examOneMongoDBContext.ExamHistories.Find(c => c.Id == model.Id).FirstOrDefaultAsync();
            if(examHistory == null)
            {
                result.Message = "Không có dữ liệu bài thi";
                return result;
            }
            //update to redis cache
            //var update = Builders<ExamHistory>.Update.Set(x => x.SelectedAnswers, model.ExamAnswer);
            //await _examOneMongoDBContext.ExamHistories.UpdateOneAsync(c => c.Id == model.Id,update);
            var redisResult = await _db.StringSetAsync($"exam:{model.Id}", model.ExamAnswer, TimeSpan.FromDays(2));
            var redisResult2 = await _db.StringSetAsync($"time:{model.Id}", now.ToString(), TimeSpan.FromDays(2));
            if (!redisResult || !redisResult2)
            {
                result.Message = "Lưu kết quả thi không thành công";
                return result;
            }
            //var answers = JsonSerializer.Deserialize<List<AnswerModel>>(model.ExamAnswer ?? "[]") ?? new List<AnswerModel>();

            //caculate score

            result.IsSuccess = true;
            return result;
        }



        public async Task<ResponderData<string>> CreateExamTest(string createBy,string branchCode)
        {
            var res = new ResponderData<string>();
            var exam = await _examOneDbContext.Exams.FirstOrDefaultAsync();
            if (exam == null)
            {
                res.Message = "Không tìm thấy đề thi";
                return res;
            }

            var examHistory = new ExamHistory
            {
                ExamId = exam.Id,
                CreatedBy = createBy,
                BranchCode = branchCode,
                IsLoad = false,
                StartDate = DateTime.Now
            };

            await _examOneMongoDBContext.ExamHistories.InsertOneAsync(examHistory);
            res.IsSuccess = true;
            res.Data = examHistory.Id;
            return res;

        }

        public async Task<ResponderData<ExamModel>> GetExamTest(string id,string createBy)
        {

            var baseChar = 'A';
            var res = new ResponderData<ExamModel>();
            var exam = await _examOneDbContext.Exams.FirstOrDefaultAsync();
            if(exam == null || string.IsNullOrEmpty(id))
            {
                res.Message = "Không tìm thấy đề thi";
                return res;
            }

            var examHistory = await _examOneMongoDBContext.ExamHistories.Find(x => x.Id == id).FirstOrDefaultAsync();
            if(examHistory == null)
            {
                res.Message = "Không tìm thấy thông tin bài thi";
                return res;
            }

            if (examHistory.IsLoad)
            {
                res.Message = "Bài thi đã được hiển thị";
                return res;
            }

            var questionBanks = new List<QuestionBank>();
            if (string.IsNullOrEmpty(examHistory.Items))
            {
                questionBanks = await _examOneDbContext.QuestionBanks
                                        .OrderBy(q => Guid.NewGuid())
                                        .Take(exam.TotalQuestions)
                                        .ToListAsync();
                var update = Builders<ExamHistory>.Update
                        .Set(x => x.IsLoad, true)
                        .Set(x => x.Items, JsonSerializer.Serialize(questionBanks));
                await _examOneMongoDBContext.ExamHistories.UpdateOneAsync(c => c.Id == id,update);
            }
            else
            {
                questionBanks = JsonSerializer.Deserialize<List<QuestionBank>>(examHistory.Items ?? "[]") ?? new List<QuestionBank>();
            }
            var data = new ExamModel
            {
                Id = id,
                ExamId = exam.Id,
                DurationMinutes = exam.DurationMinutes,
                StartTime = Constant.GetDateTimeFromMongo(examHistory.StartDate)
            };

            var index = 1;
            foreach (var item in questionBanks)
            {
                var questionModel = new ExamQuestionModel
                {
                    Id = item.Id,
                    QuestionIndex = index++,
                    Question = item.Question,
                    Description = item.Description,
                    Article = item.Article,
                    Explanation = item.Explanation
                };

                questionModel.Items = JsonSerializer.Deserialize<List<OptionModel>>(item.Items ?? "[]") ?? new List<OptionModel>();
                //questionModel.Items = questionModel.Items.OrderBy(c => c.SortOrder).ToList();
                data.Questions.Add(questionModel);

                var correctAnswer = questionModel.Items.FirstOrDefault(c => c.IsCorrect);
                if (correctAnswer == null)
                {
                    res.Message = "Dữ liệu đáp án không hợp lệ";
                    return res;
                }
                questionModel.AnsweredOptionId = correctAnswer.Id;
                char character = (char)(baseChar + correctAnswer.SortOrder);
                questionModel.AnsweredOptionCharacter = character.ToString();
                questionModel.AnsweredOptionIndex = correctAnswer.SortOrder;
            }

            res.IsSuccess = true;
            res.Data = data;
            return res;
        }

        public async Task<ResponderData<ExamModel>> GetIntructionExam(string createBy)
        {
            var result = new ResponderData<ExamModel>();
            var data = await _examOneDbContext.Exams.FirstOrDefaultAsync();
            if(data == null)
            {
                result.Message = "Không tìm thấy thông tin đề thi";
                return result;
            }

            var resultExam = await _examOneMongoDBContext.ExamHistories.Find(c => c.ExamId == data.Id && c.CreatedBy == createBy).FirstOrDefaultAsync();

            result.Data = new ExamModel
            {
                DurationMinutes = data.DurationMinutes,
                Instructions = data.Instructions,
                TotalQuestions = data.TotalQuestions,
                IsComplete = resultExam != null ? true : false,
                ExamId = data.Id
            };
            result.IsSuccess = true;
            return result;
        }

        public ResponderData<ResultExamModel> ResultExam(ExamModel model, string selectedAnswers)
        {
            var result = new ResponderData<ResultExamModel>();
            if (model == null || string.IsNullOrEmpty(selectedAnswers))
            {
                result.Message = "Dữ liệu không hợp lệ";
                return result;
            }
            var answers = JsonSerializer.Deserialize<List<AnswerModel>>(selectedAnswers ?? "[]") ?? new List<AnswerModel>();
            var data = new ResultExamModel();
            data.TotalQuestions = model.Questions.Count;
            data.CorrectAnswer = model.Questions.Where(c => answers.FirstOrDefault(x => x.id == c.Id && x.answerId == c.AnsweredOptionId) != null).Count();
            data.TotalAnswer = answers.Where(c => !string.IsNullOrEmpty(c.answerId)).Count();
            result.Data = data;
            result.IsSuccess = true;

            return result;
        }

        public async Task<ResponderData<string>> SettingExam(ExamModel model)
        {
            var result = new ResponderData<string>();   
            if(model == null)
            {
                result.Message = "Dữ liệu không hợp lệ";
                return result;
            }

            if(model.DurationMinutes <= 0)
            {
                result.Message = "Thời gian làm bài không hợp lệ";
                return result;
            }

            if (model.TotalQuestions <= 0)
            {
                result.Message = "Số lượng câu hỏi làm bài không hợp lệ";
                return result;
            }

            if (string.IsNullOrEmpty(model.Instructions))
            {
                result.Message = "Chỉ dẫn không được để trống";
                return result;
            }
            var exam = await _examOneDbContext.Exams.FirstOrDefaultAsync();
            if (exam == null)
            {
                exam = new Exam
                {
                    DurationMinutes = model.DurationMinutes,
                    Instructions = model.Instructions,
                    TotalQuestions = model.TotalQuestions
                };
                _examOneDbContext.Exams.Add(exam);
            }
            else
            {
                exam.DurationMinutes = model.DurationMinutes;
                exam.Instructions = model.Instructions;
                exam.TotalQuestions = model.TotalQuestions;          
            }
            await _examOneDbContext.SaveChangesAsync();
            result.IsSuccess = true;
            result.Message = "Cập nhật thành công";
            return result;
        }

        public async Task<ResponderData<ExamProgressModel>> GetListExamHistory()
        {
            var response = new ResponderData<ExamProgressModel>();

            var listExamHistory = await _examOneMongoDBContext.ExamHistories
                .Find(_ => true)
                .Sort(Builders<ExamHistory>.Sort.Descending(x => x.StartDate))
                .ToListAsync();

            var branches = await _examOneDbContext.Branches.ToListAsync();

            foreach (var item in listExamHistory)
            {
                var branch = branches.FirstOrDefault(c => c.Id.ToString() == item.BranchCode);
                if(branch == null)
                {
                    response.Message = "Dữ liệu không hợp lệ";
                    return response;
                }

                var user = await _userManager.FindByNameAsync(item.CreatedBy);
                if(user == null)
                {
                    response.Message = "Dữ liệu không hợp lệ";
                    return response;
                }
                var item1 = new ExamProgressModel
                {
                    Id = item.Id,
                    StartDate = item.StartDate,
                    BranchCode = item.BranchCode,
                    BranchName = Constant.GetLocation(branch.Name),
                    BranchNameShort = branch.Name,
                    CreatedBy = item.CreatedBy,
                    CreatedFullName = user.FullName,
                };

                if (!string.IsNullOrEmpty(item.Items) && !string.IsNullOrEmpty(item.SelectedAnswers))
                {
                    item1.ExamStatus = ExamStatus.Done;
                    item1.TotalCorrectAnswers = item.TotalCorrectAnswers;
                    item1.ComplatedDuration = GetDurationString(item.ComplatedDuration);
                    item1.EndDate = item.EndDate;
                    item1.MarkDate = item.MarkDate;
                }
                else if(!string.IsNullOrEmpty(item.Items)) item1.ExamStatus = ExamStatus.InProgress;
                else item1.ExamStatus = ExamStatus.NotYet;

                response.DataList.Add(item1);
            }
            response.IsSuccess = true;
            return response;
        }

        private string GetDurationString(double totalMiliseconds)
        {
            var timeSpan = TimeSpan.FromMilliseconds(totalMiliseconds);
            string result = $"{(int)timeSpan.TotalMinutes}m {timeSpan.Seconds}s";
            return result;
        }

        public async Task<ResponderData<ExamProgressModel>> GetExamDetail(string id)
        {
            var result = new ResponderData<ExamProgressModel>();

            var examHistory = await _examOneMongoDBContext.ExamHistories.Find(c => c.Id == id).FirstOrDefaultAsync();
            if(examHistory == null)
            {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }
            var branch = await _examOneDbContext.Branches.FirstOrDefaultAsync(c => c.Id.ToString() == examHistory.BranchCode);
            var user = await _userManager.FindByNameAsync(examHistory.CreatedBy);
            if (user == null || branch == null)
            {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }
            var item1 = new ExamProgressModel
            {
                Id = examHistory.Id,
                StartDate = Constant.GetDateTimeFromMongo(examHistory.StartDate),
                BranchCode = examHistory.BranchCode,
                BranchName = Constant.GetLocation(branch.Name),
                BranchNameShort = branch.Name,
                CreatedBy = examHistory.CreatedBy,
                CreatedFullName = user.FullName,
            };

            if (!string.IsNullOrEmpty(examHistory.Items) && !string.IsNullOrEmpty(examHistory.SelectedAnswers))
            {
                item1.ExamStatus = ExamStatus.Done;
                item1.TotalCorrectAnswers = examHistory.TotalCorrectAnswers;
                item1.ComplatedDuration = GetDurationString(examHistory.ComplatedDuration);
                item1.EndDate = Constant.GetDateTimeFromMongo(examHistory.EndDate);
                item1.MarkDate = Constant.GetDateTimeFromMongo(examHistory.MarkDate);
                var answers = JsonSerializer.Deserialize<List<AnswerModel>>(examHistory.SelectedAnswers ?? "[]") ?? new List<AnswerModel>();
                item1.SelectedAnswers = answers;
                var questionBanks = JsonSerializer.Deserialize<List<QuestionBank>>(examHistory.Items ?? "[]") ?? new List<QuestionBank>();
                foreach (var item in questionBanks)
                {
                    var baseChar = 'A';
                    var options = JsonSerializer.Deserialize<List<OptionModel>>(item.Items ?? "[]") ?? new List<OptionModel>();

                    var correctAnswer = options.FirstOrDefault(c => c.IsCorrect);
                    if(correctAnswer == null)
                    {
                        result.Message = "Câu hỏi không hợp lệ";
                        return result;
                    }
                    char character = (char)(baseChar + correctAnswer.SortOrder);
                    var userAnswer = answers.FirstOrDefault(c => c.id == item.Id);
                    string characterUser2 = string.Empty;
                    if (userAnswer != null && !string.IsNullOrEmpty(userAnswer.answerId))
                    {
                        var userAnswer2 = options.FirstOrDefault(c => c.Id == userAnswer.answerId);
                        if (userAnswer2 == null)
                        {
                            result.Message = "Đáp án không hợp lệ";
                            return result;
                        }
                        var characterUser = (char)(character + userAnswer2.SortOrder);
                        characterUser2 = characterUser.ToString();
                    }
                    else
                    {
                        characterUser2 = "No data";
                    }

                    item1.Questions.Add(new ExamQuestionModel
                    {
                        Id = item.Id,
                        Article = item.Article,
                        Description = item.Description,
                        Question = item.Question,
                        Explanation = item.Explanation,
                        Items = options,
                        AnsweredOptionId = correctAnswer.Id,
                        AnsweredOptionCharacter = character.ToString(),
                        AnsweredOptionIndex = correctAnswer.SortOrder,
                        UserAnsweredOptionId = userAnswer != null ? userAnswer.answerId : null,
                        Point = userAnswer != null && correctAnswer.Id == userAnswer.answerId ? 1 : 0,
                        UserAnsweredOptionCharacter = characterUser2
                    });
                }
            }
            else if (!string.IsNullOrEmpty(examHistory.Items))
            {
                var questionBanks = JsonSerializer.Deserialize<List<QuestionBank>>(examHistory.Items ?? "[]") ?? new List<QuestionBank>();
                foreach (var item in questionBanks)
                {
                    var baseChar = 'A';
                    var options = JsonSerializer.Deserialize<List<OptionModel>>(item.Items ?? "[]") ?? new List<OptionModel>();

                    var correctAnswer = options.FirstOrDefault(c => c.IsCorrect);
                    if (correctAnswer == null)
                    {
                        result.Message = "Câu hỏi không hợp lệ";
                        return result;
                    }
                    char character = (char)(baseChar + correctAnswer.SortOrder);
                    item1.Questions.Add(new ExamQuestionModel
                    {
                        Id = item.Id,
                        Article = item.Article,
                        Description = item.Description,
                        Question = item.Question,
                        Explanation = item.Explanation,
                        Items = options,
                        AnsweredOptionId = correctAnswer.Id,
                        AnsweredOptionCharacter = character.ToString(),
                        AnsweredOptionIndex = correctAnswer.SortOrder
                    });
                }
                item1.ExamStatus = ExamStatus.InProgress;
            }
            else item1.ExamStatus = ExamStatus.NotYet;

            result.IsSuccess = true;
            result.Data = item1;
            return result;
        }

        public async Task<ResponderData<string>> RemoveExamHistory(string id)
        {
            var result = new ResponderData<string>();
            var examHistory = await _examOneMongoDBContext.ExamHistories.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (examHistory == null)
            {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }
            var filter = Builders<ExamHistory>.Filter.Eq("_id", new ObjectId(id));
            await _examOneMongoDBContext.ExamHistories.DeleteOneAsync(filter);
            result.Message = "Xóa thành công";
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResponderData<string>> GetExamData(string key)
        {
            var result = new ResponderData<string>();
            var examData = await _db.StringGetAsync($"{key}");
            if (string.IsNullOrEmpty(examData)) return result;
            result.Data = examData;
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResponderData<string>> SetExamData(string key, string data)
        {
            var result = new ResponderData<string>();
            var isDone = await _db.StringSetAsync($"{key}",data);
            result.IsSuccess = isDone;
            return result;
        }

        public async Task<ResponderData<string>> AddData()
        {
            var result1 = new ResponderData<string>();
            try
            {
                var result = new List<QuestionBank>();
                var options = new List<string>();
                string correctAnswer = null;
                var path = Path.Combine(_env.ContentRootPath, "data", "result.xlsx");

                using var workbook = new XLWorkbook(path);
                var ws = workbook.Worksheet(1);
                int lastRow = ws.LastRowUsed().RowNumber();
                int row = 3;
                while (row <= lastRow)
                {
                    QuestionBank current = null;
                    while (true)
                    {
                        var raw = ws.Cell(row, 1).GetString();

                        if (string.IsNullOrWhiteSpace(raw))
                            break;

                        var cell = Normalize(raw);

                        // 🔥 Câu hỏi
                        if (Regex.IsMatch(cell, @"^Câu\s*\d+", RegexOptions.IgnoreCase))
                        {
                            // save câu trước
                            if (current != null)
                            {
                                current.Items = BuildItems(options, correctAnswer);
                                result.Add(current);
                            }

                            current = new QuestionBank
                            {
                                Question = cell,
                                CreateDate = DateTime.Now,
                                CreatedBy = "admin",
                                Type = "SingleChoice",
                                Article = "<p><br></p>",
                                Explanation = "<p><br></p>",
                                Description = ""
                            };

                            options = new List<string>();
                            correctAnswer = null;
                        }
                        // 🔥 Option (A. B. C. D. E...)
                        else if (Regex.IsMatch(cell, @"^[A-Z]\.", RegexOptions.IgnoreCase))
                        {
                            options.Add(cell);
                        }
                        // 🔥 Đáp án đúng
                        else if (cell.StartsWith("Đáp án đúng", StringComparison.OrdinalIgnoreCase))
                        {
                            var match = Regex.Match(cell, @"[A-Z]");

                            if (match.Success)
                            {
                                correctAnswer = match.Value.ToUpper();
                                row++;
                                break;
                            }
                        }

                        row++;

                    }

                    // 👉 thêm câu cuối
                    current.Items = BuildItems(options, correctAnswer);
                    result.Add(current);

                }

                _examOneDbContext.QuestionBanks.AddRange(result);
                await _examOneDbContext.SaveChangesAsync();
                return result1;

            }
            catch (Exception ex)
            {

                throw;
            }
            //return result1;
        }

        // 🔥 Normalize text (xử lý HTML + spacing)
        private string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // remove HTML nếu có
            input = Regex.Replace(input, "<.*?>", "");

            // normalize space
            input = Regex.Replace(input, @"\s+", " ");

            return input.Trim();
        }

        // 🔥 Build JSON Items
        private string BuildItems(List<string> options, string correctAnswer)
        {
            var items = new List<object>();

            for (int i = 0; i < options.Count; i++)
            {
                var option = options[i];

                // lấy key A/B/C...
                var key = option.Substring(0, 1).ToUpper();

                // lấy text sau A.
                var text = Regex.Replace(option, @"^[A-Z]\.\s*", "").Trim();

                items.Add(new
                {
                    SortOrder = i,
                    Id = Guid.NewGuid().ToString(),
                    OptionText = text,
                    IsCorrect = key == correctAnswer
                });
            }

            return JsonSerializer.Serialize(items);
        }
    }
}
