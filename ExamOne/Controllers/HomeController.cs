using ExamOne.Models;
using ExamOne.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization.Serializers;


//using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExamOne.Controllers
{
    [Route("")]
    [CustomAuthorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private IExamService _examService;
        private IScoreService _scoreService;
        private readonly IDistributedCache _cache;
        private AesHelper _aes;
        public HomeController(IExamService examService, IScoreService scoreService
            , IDistributedCache cache, AesHelper aes)
        {
            _cache = cache;
            _scoreService = scoreService;
            //_logger = logger;
            _examService = examService;
            _aes = aes;
        }


        [Route("thong-tin-ca-nhan")]
        public async Task<IActionResult> Profile()
        {
            var createdBy = User.Identity?.Name;
            var result = await _scoreService.GetProfile(createdBy);
            return View(result.Data);
        }

        [Route("doi-mat-khau")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var top10RankingPersons = await _scoreService.GetRankingsPerson(10);
            ViewBag.Top10RankingPersons = top10RankingPersons.DataList;

            var top10RankingBranches = await _scoreService.GetRankingsBranch(10);
            ViewBag.Top10RankingBranches = top10RankingBranches.DataList;

            //var intructions = await _examService.GetIntructionExam("");

            //var data = await _examService.AddData();
            //ViewBag.Intructions = intructions.Data.Instructions;
            return View();
        }

        [Route("bai-thi")]
        public async Task<IActionResult> Exam()
        {
            Response.Cookies.Delete(_aes.ExamDataKey);
            Response.Cookies.Delete(_aes.AnswerClientDataKey);
            var createdBy = User.Identity?.Name;
            var result = await _examService.GetIntructionExam(createdBy);
            ViewBag.IsSuccess = result.IsSuccess;
            return View(result);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("bai-thi")]
        public async Task<IActionResult> StartExam()
        {
            var userName = User.Identity?.Name;
            var branchCode = User.FindFirst("BranchCode")?.Value ?? string.Empty;
            var result = await _examService.CreateExamTest(userName, branchCode);
            return Json(result);
        }

        [Route("bai-thi/tom-tat")]
        public async Task<IActionResult> SummaryExam(string sId)
        {
            var key = $"questions:{sId}";
            //var examDataResult = Request.Cookies[_aes.ExamDataKey];
            var examDataResult = await _examService.GetExamData(key);
            var examData = examDataResult.IsSuccess ? examDataResult.Data : string.Empty;
            if (string.IsNullOrEmpty(examData))
            {
                return Redirect("/tai-khoan/truy-cap");
            }
            return View();
        }

        [Route("bai-thi/hoan-tat")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CompleteExam(ExamAnswerModel model)
        {
            var result = await _examService.CompleteExam(model);
            return Json(result);
        }


        [Route("bai-thi/ket-thuc")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DoneExam(DoneExamRequest model)
        {
            var result = new ResponderData<string>();
            if(string.IsNullOrEmpty(model.sId) || model.estimateValue < 0)
            {
                result.Message = "Dữ liệu dự đoán không hợp lệ";
                return Json(result);
            }

            if(model.estimateValue == 0)
            {
                result.IsSuccess = true;
                return Json(result);
            }
            var createBy = User.Identity?.Name;
            var key = $"estimate:{createBy}";
            var resultData = await _examService.SetExamData(key, model.estimateValue.ToString());
            return Json(resultData);
        }

        [Route("bai-thi/ket-qua")]
        public async Task<IActionResult> ResultExam(string sId)
        {
            ViewBag.SId = sId;
            //return View(new ResultExamModel());
            var key = $"questions:{sId}";
            var examDataResult = await _examService.GetExamData(key);
            var examData = examDataResult.IsSuccess ? examDataResult.Data : string.Empty;

            var key2 = $"exam:{sId}";
            var answerDataResult = await _examService.GetExamData(key2);
            var answerData = answerDataResult.IsSuccess ? answerDataResult.Data : string.Empty;

            if (string.IsNullOrEmpty(examData) || string.IsNullOrEmpty(answerData))
            {
                return Redirect("/tai-khoan/truy-cap");
            }
            var createBy = User.Identity?.Name;
            var key3 = $"estimate:{createBy}";
            var resultRedis3 = await _examService.GetExamData(key3);
            if (resultRedis3.IsSuccess && !string.IsNullOrEmpty(resultRedis3.Data))
            {
                ViewBag.IsEstimate2 = resultRedis3.Data;
            }
            else ViewBag.IsEstimate2 = "False";

            var result = new ResponderData<ExamModel>();
            result.IsSuccess = true;
            result.Data = JsonSerializer.Deserialize<ExamModel>(examData) ?? new ExamModel();
            var response = _examService.ResultExam(result.Data, answerData);
            return View(response.Data);
        }

        [Route("bai-thi/khoi-dong")]
        public async Task<IActionResult> DetailQuestion(string sId)
        {
            var key = $"questions:{sId}";
            var result = new ResponderData<ExamModel>();
            var examDataResult = await _examService.GetExamData(key);
            var examData = examDataResult.IsSuccess ? examDataResult.Data : string.Empty;
            if (string.IsNullOrEmpty(examData))
            {
                var userName = User.Identity?.Name;
                result = await _examService.GetExamTest(sId, userName);
                if (result.IsSuccess)
                {
                    var resultData = await _examService.SetExamData(key, JsonSerializer.Serialize(result.Data));
                    if(!resultData.IsSuccess) return Redirect("/tai-khoan/truy-cap");
                }
                else return Redirect("/tai-khoan/truy-cap");
            }
            else
            {
                result.IsSuccess = true;
                result.Data = JsonSerializer.Deserialize<ExamModel>(examData) ?? new ExamModel();
            }
            ViewBag.SId = sId;
            return View(result.Data);
        }
    }
}
