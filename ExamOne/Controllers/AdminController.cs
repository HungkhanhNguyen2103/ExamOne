using ExamOne.Models;
using ExamOne.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExamOne.Controllers
{
    [Route("quan-ly")]
    [AdminOnly]
    public class AdminController : Controller
    {
        private IQuestionService _questionService;
        private IExamService _examService;
        private IScoreService _scoreService;
        public AdminController(IQuestionService questionService, IExamService examService, IScoreService scoreService)
        {
            _questionService = questionService;
            _examService = examService;
            _scoreService = scoreService;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var top10RankingPersons = await _scoreService.GetRankingsPerson(-1);
            ViewBag.RankingPersons = top10RankingPersons.DataList;

            var top10RankingBranches = await _scoreService.GetRankingsBranch(-1);
            ViewBag.RankingBranches = top10RankingBranches.DataList;
            return View();
        }

        [Route("cai-dat")]
        [HttpGet]
        public async Task<IActionResult> Setting()
        {
            var createdBy = User.Identity?.Name;
            var result = await _examService.GetIntructionExam("");
            return View(result.Data);
        }

        [Route("cai-dat")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Setting(ExamModel model)
        {
            var result = await _examService.SettingExam(model);
            return Json(result);
        }

        [Route("ngan-hang-cau-hoi")]
        [HttpGet]
        public async Task<IActionResult> ListQuestionBank()
        {
            var result = await _questionService.GetExamQuestions();
            return View(result.DataList);
        }

        [Route("danh-sach-bai-thi")]
        [HttpGet]
        public async Task<IActionResult> ExamResult()
        {
            var result = await _examService.GetListExamHistory();
            return View(result.DataList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("bai-thi/{id}")]
        public async Task<IActionResult> ExamDetail(string id)
        {
            var result = await _examService.GetExamDetail(id);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("xoa-bai-thi/{id}")]
        public async Task<IActionResult> RemoveExam(string id)
        {
            var result = await _examService.RemoveExamHistory(id);
            return Json(result);
        }

        [Route("them-cau-hoi")]
        [HttpGet]
        public IActionResult QuestionBank()
        {
            ViewBag.Id = 0;
            return View(new ExamQuestionModel { });
        }

        [Route("cap-nhat-cau-hoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionBank(ExamQuestionModel model)
        {
            model.CreatedBy = User.Identity?.Name;
            var result = await _questionService.UpdateQuestionBank(model);
            return Json(result);
        }

        [Route("cau-hoi/{id}")]
        [HttpGet]
        public async Task<IActionResult> QuestionBank(int id)
        {
            var result = await _questionService.GetQuestionBank(id);
            ViewBag.Id = id;
            return View(result.Data);
        }

        [Route("cau-hoi/{id}")]
        [HttpPost]
        public async Task<IActionResult> QuestionBankDetail(int id)
        {
            var result = await _questionService.GetQuestionBank(id);
            return Json(result);
        }

        [Route("xoa-cau-hoi/{id}")]
        [HttpPost]
        public async Task<IActionResult> RemoveQuestionBank(int id)
        {
            var result = await _questionService.DeleteQuestionBank(id);
            return Json(result);
        }
    }
}
