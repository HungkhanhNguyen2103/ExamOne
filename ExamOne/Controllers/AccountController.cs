using ExamOne.Models;
using ExamOne.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExamOne.Controllers
{
    [Route("tai-khoan")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("dang-nhap")]
        [HttpGet]
        [AuthenVerify]
        public IActionResult Login()
        {
            return View();
        }

        [Route("dang-nhap")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(AccountModel model)
        {
            var result = await _accountService.Login(model);
            return Json(result);
        }

        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("thong-tin-ca-nhan")]
        public async Task<IActionResult> Profile(ProfileModel model)
        {
            model.UserName = User.Identity?.Name;
            var result = await _accountService.UpdateProfile(model);
            return Json(result);
        }

        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("doi-mat-khau")]
        public async Task<IActionResult> ChangePassword(PasswordModel model)
        {
            model.CreateBy = User.Identity?.Name;
            var result = await _accountService.ChangePassword(model);
            return Json(result);
        }

        [Route("dang-ki")]
        [HttpGet]
        [AuthenVerify]
        public async Task<IActionResult> Register()
        {
            var result1 = await _accountService.GetBranches();
            ViewBag.Branches = result1.DataList;
            return View();
        }

        [Route("dang-ki")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(AccountModel model)
        {
            var result = await _accountService.Register(model);
            return Json(result);
        }

        [Route("dang-xuat")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [CustomAuthorize]
        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            var result = await _accountService.Logout();
            return Json(result);
        }

        [HttpGet]
        [Route("truy-cap")]
        public IActionResult AccessDenied()
        {
           return View();
        }
    }
}
