using ExamOne.Entity;
using ExamOne.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;


namespace ExamOne.Service
{
    public interface IQuestionService
    {
        Task<ResponderData<ExamQuestionModel>> GetExamQuestions();
        Task<ResponderData<ExamQuestionModel>> GetQuestionBank(int id);
        Task<ResponderData<string>> UpdateQuestionBank(ExamQuestionModel model);
        Task<ResponderData<string>> DeleteQuestionBank(int id);
    }
    public class QuestionService : IQuestionService
    {
        private ExamOneDbContext _examOneDbContext;
        public QuestionService(ExamOneDbContext examOneDbContext)
        {
            _examOneDbContext = examOneDbContext;
        }

        public async Task<ResponderData<string>> DeleteQuestionBank(int id)
        {
            var result = new ResponderData<string>();
            var questionBank = await _examOneDbContext.QuestionBanks.FirstOrDefaultAsync(c => c.Id == id);
            if(questionBank == null)
            {
                result.Message = "Dữ liệu không hợp lệ";
                return result;
            }
            _examOneDbContext.QuestionBanks.Remove(questionBank);
            await _examOneDbContext.SaveChangesAsync();
            result.Message = "Xóa thành công";
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResponderData<ExamQuestionModel>> GetExamQuestions()
        {
            var res = new ResponderData<ExamQuestionModel>();
            var data = await _examOneDbContext.QuestionBanks.OrderByDescending(c => c.CreateDate).ToListAsync();
            foreach (var item in data) {
                res.DataList.Add(new ExamQuestionModel
                {
                    Id = item.Id,
                    Question = item.Question,
                    Description = item.Description,
                    Article = item.Article,
                    Explanation = item.Explanation,    
                    CreateDate = item.CreateDate
                    //Items = JsonSerializer.Deserialize<List<OptionModel>>(item.Items ?? "[]") ?? new List<OptionModel>(),
                });
            }
            res.IsSuccess = true;
            return res;
        }

        public async Task<ResponderData<ExamQuestionModel>> GetQuestionBank(int id)
        {
            char baseChar = 'A';
            var result = new ResponderData<ExamQuestionModel>();
            var questionBank = await _examOneDbContext.QuestionBanks.FirstOrDefaultAsync(c => c.Id == id);
            if(questionBank == null)
            {
                result.Message = "Dữ liệu không hợp lệ";
                return result;
            }
            result.IsSuccess = true;
            result.Data = new ExamQuestionModel
            {
                Id = questionBank.Id,
                Article = questionBank.Article,
                Question = questionBank.Question,
                CreateDate = questionBank.CreateDate,
                CreatedBy = questionBank.CreatedBy,
                Explanation = questionBank.Explanation,
                Description = questionBank.Description,
                CreateDateStr = questionBank.CreateDate.ToString("HH:mm dd/MM/yyyy"),
                Items = JsonSerializer.Deserialize<List<OptionModel>>(questionBank.Items ?? "[]") ?? new List<OptionModel>(),
            };
            var correctAnswer = result.Data.Items.FirstOrDefault(c => c.IsCorrect);
            if(correctAnswer == null)
            {
                result.Message = "Dữ liệu đáp án không hợp lệ";
                return result;
            }
            result.Data.AnsweredOptionId = correctAnswer.Id;
            char character = (char)(baseChar + correctAnswer.SortOrder);
            result.Data.AnsweredOptionCharacter = character.ToString();
            result.Data.AnsweredOptionIndex = correctAnswer.SortOrder;
            return result;
        }

        public async Task<ResponderData<string>> UpdateQuestionBank(ExamQuestionModel model)
        {
            var result = new ResponderData<string>();
            if(model == null)
            {
                result.Message = "Dữ liệu không hợp lệ";
                return result;
            }
            if (string.IsNullOrEmpty(model.Question))
            {
                result.Message = "Dữ liệu câu hỏi không được để trống";
                return result;
            }

            if (string.IsNullOrEmpty(model.Options))
            {
                result.Message = "Dữ liệu đáp án không được để trống";
                return result;
            }

            if (string.IsNullOrEmpty(model.AnsweredOptionId))
            {
                result.Message = "Không có dữ liệu đáp án đúng";
                return result;
            }
            var type = "SingleChoice";
            var questionBank = await _examOneDbContext.QuestionBanks.FirstOrDefaultAsync(q => q.Id == model.Id);
            if(questionBank == null)
            {
                questionBank = new QuestionBank
                {
                    Question = model.Question,
                    Description = model.Description,
                    Items = model.Options,
                    Explanation = model.Explanation,
                    Article = model.Article,
                    CreatedBy = model.CreatedBy,
                    Type = type,
                    CreateDate = DateTime.Now
                };
                _examOneDbContext.QuestionBanks.Add(questionBank);
            }
            else
            {
                questionBank.Question = model.Question;
                questionBank.Description = model.Description;
                questionBank.Items = model.Options;
                questionBank.Explanation = model.Explanation;
                questionBank.Article = model.Article;
                questionBank.CreatedBy = model.CreatedBy;
                questionBank.CreateDate = DateTime.Now;
                questionBank.Type = type;
            }
            await _examOneDbContext.SaveChangesAsync();
            result.IsSuccess = true;
            result.Message = "Cập nhật câu hỏi thành công";
            result.Data = questionBank.Id.ToString();
            return result;
        }
    }
}
