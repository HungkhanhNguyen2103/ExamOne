using ExamOne.Entity;
using ExamOne.Helper;
using ExamOne.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Text.Json;
using ZstdSharp.Unsafe;

namespace ExamOne.Service
{
    public interface IScoreService
    {
        Task<ResponderData<RankingModel>> GetRankingsPerson(int limit);
        Task<ResponderData<RankingBranchModel>> GetRankingsBranch(int limit);
        Task<ResponderData<ProfileModel>> GetProfile(string username);
    }
    public class ScoreService : IScoreService
    {
        private readonly UserManager<Account> _userManager;
        private ExamOneDbContext _examOneDbContext;
        private ExamOneMongoDBContext _examOneMongoDBContext;
        public ScoreService(ExamOneDbContext examOneDbContext,ExamOneMongoDBContext examOneMongoDBContext, UserManager<Account> userManager)
        {
            _examOneDbContext = examOneDbContext;
            _examOneMongoDBContext = examOneMongoDBContext;
            _userManager = userManager;
        }

        public async Task<ResponderData<ProfileModel>> GetProfile(string username)
        {
            var result = new ResponderData<ProfileModel>();
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }
            var branch = await _examOneDbContext.Branches.FirstOrDefaultAsync(c => c.Id.ToString() == user.BranchCode);
            if (branch == null)
            {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }
            var random = DateTime.Now.Ticks;
            var data = new ProfileModel
            {
                UserName = username,
                Avatar = $"{user.Avatar}?v={random}",
                FullName = user.FullName,
                CCCD = user.CCCD,
                Location = Constant.GetLocation(branch.Name),
            };
            var examResultList = await _examOneMongoDBContext.ExamHistories.Find(c => c.CreatedBy == username).SortBy(x => x.StartDate).ToListAsync();
            if(examResultList.Count >= 3) data.ExamStatus = ProfileStatus.Marked;
            data.ExamDisplays = examResultList.Select((c,ind) => new ExamDisplayModel
            {
                ID = c.Id,
                Index = ind,
                CorrectAnswer = c.TotalCorrectAnswers,
                TotalAnswer = GetTotalQuestion(c.Items),
                Duration = GetDurationString(c.ComplatedDuration),
                CreateDate = c.StartDate.AddHours(7).ToString("dd/MM/yyyy - HH:mm")
            }).ToList();
            //if (examResult == null) data.ExamStatus = ProfileStatus.NotYet;
            //else if (string.IsNullOrEmpty(examResult.SelectedAnswers)) data.ExamStatus = ProfileStatus.Done;
            //else if (!string.IsNullOrEmpty(examResult.SelectedAnswers)) data.ExamStatus = ProfileStatus.Marked;
            //data.ExamStatus = ProfileStatus.Marked;
            var examResult = examResultList
                .OrderByDescending(x => x.TotalCorrectAnswers)
                .ThenBy(x => x.ComplatedDuration)
                .FirstOrDefault();

            if (examResult != null)
            {
                data.TotalCorrectAnswer = examResult.TotalCorrectAnswers;
                data.DurationTime = GetDurationString(examResult.ComplatedDuration);

                var questions = JsonSerializer.Deserialize<List<QuestionBank>>(examResult.Items ?? "[]") ?? new List<QuestionBank>();
                data.TotalQuestions = questions.Count;

                if (data.ExamStatus == ProfileStatus.Marked)
                {
                    var bestPerUser = await _examOneMongoDBContext.ExamHistories.Aggregate()
                        .Match(x => x.ExamId == examResult.ExamId)
                        .SortByDescending(x => x.TotalCorrectAnswers)
                        .ThenBy(x => x.ComplatedDuration)
                        .Group(x => x.CreatedBy, g => new
                        {
                            UserId = g.Key,
                            Best = g.First()
                        })
                        .Project(x => x.Best)
                        .ToListAsync();

                    var betterCount = bestPerUser.Count(x =>
                        x.TotalCorrectAnswers > examResult.TotalCorrectAnswers ||
                        (x.TotalCorrectAnswers == examResult.TotalCorrectAnswers &&
                         x.ComplatedDuration < examResult.ComplatedDuration)
                    );

                    data.Rank = betterCount + 1;
                }
            }

            result.Data = data;   
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResponderData<RankingBranchModel>> GetRankingsBranch(int limit)
        {
            var response = new ResponderData<RankingBranchModel>();

            var branchs = await _examOneDbContext.Branches.ToListAsync();

            var result = await _examOneMongoDBContext.ExamHistories.Find(c => !string.IsNullOrEmpty(c.SelectedAnswers)).ToListAsync();

            int rank = 1;
            foreach (var branch in branchs)
            {
                var item = new RankingBranchModel
                {
                    BranchCode = branch.Id.ToString(),
                    BranchName = branch.Name,               
                };

                item.Users = result.Where(c => c.BranchCode == branch.Id.ToString()).Select(c => new RankingModel
                {
                    UserName = c.CreatedBy,
                    CompletionTime = GetDurationString(c.ComplatedDuration),
                    CorrectAnswer = c.TotalCorrectAnswers,
                    ComplatedDuration = c.ComplatedDuration
                }).ToList();

                if (item.Users.Count > 0)
                {
                    var avgCorrectAnswer = item.Users.Average(c => c.CorrectAnswer);
                    item.AverageComplatedDuration = item.Users.Average(c => c.ComplatedDuration);
                    item.AverageCompletionTime = GetDurationString(item.AverageComplatedDuration);
                    item.AverageCorrectAnswer = avgCorrectAnswer;
                    item.AverageCorrectAnswerCeil = RoundTwoDecimals(avgCorrectAnswer);
                }
                else item.AverageCorrectAnswerCeil = "0";
                response.DataList.Add(item);
            }
            var resultList = response.DataList.OrderByDescending(c => c.AverageCorrectAnswer).ThenBy(c => c.AverageComplatedDuration).Select(c => new RankingBranchModel
            {
                Rank = rank++,
                BranchCode = c.BranchCode,
                BranchName = c.BranchName,
                BranchNameFull = Constant.GetLocation(c.BranchName),
                AverageComplatedDuration = c.AverageComplatedDuration,
                AverageCompletionTime = c.AverageCompletionTime,
                AverageCorrectAnswer = c.AverageCorrectAnswer,
                AverageCorrectAnswerCeil = c.AverageCorrectAnswerCeil,
                Users = c.Users
            });

            if(limit != -1)
            {
                resultList = resultList.Take(limit);
            }
            var resultList2 = resultList.ToList();
            response.DataList = resultList2;
            response.IsSuccess = true;
            return response;
        }

        public async Task<ResponderData<RankingModel>> GetRankingsPerson(int limit)
        {
            var response = new ResponderData<RankingModel>();

            var branches = await _examOneDbContext.Branches.ToListAsync();

            var bestPerUser = await _examOneMongoDBContext.ExamHistories.Aggregate()
                .Match(x => !string.IsNullOrEmpty(x.SelectedAnswers))
                .SortByDescending(x => x.TotalCorrectAnswers)
                .ThenBy(x => x.ComplatedDuration)
                .Group(x => x.CreatedBy, g => new
                {
                    UserId = g.Key,
                    Best = g.First()
                })
                .Project(x => x.Best)
                .SortByDescending(x => x.TotalCorrectAnswers)
                .ThenBy(x => x.ComplatedDuration)
                .ToListAsync();

            if (limit != -1)
            {
                bestPerUser = bestPerUser.Take(limit).ToList();
            }

            int rank = 1;

            foreach (var item in bestPerUser)
            {
                var user = await _userManager.FindByNameAsync(item.CreatedBy);

                if (user != null)
                {
                    var branch = branches.FirstOrDefault(c => c.Id.ToString() == user.BranchCode);
                    var random = DateTime.Now.Ticks;

                    response.DataList.Add(new RankingModel
                    {
                        Rank = rank++,
                        Avatar = $"{user.Avatar}?v={random}",
                        UserName = item.CreatedBy,
                        Name = user.FullName,
                        CorrectAnswer = item.TotalCorrectAnswers,
                        BranchName = branch != null ? Constant.GetLocation(branch.Name) : "No data",
                        CompletionTime = GetDurationString(item.ComplatedDuration)
                    });
                }
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

        private int GetTotalQuestion(string items)
        {
            var questions = JsonSerializer.Deserialize<List<QuestionBank>>(items ?? "[]") ?? new List<QuestionBank>();
            return questions.Count;
        }

        private string RoundTwoDecimals(double value)
        {
            var result = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return result.ToString();
        }
    }
}
