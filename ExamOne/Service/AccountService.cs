using Azure;
using ExamOne.Entity;
using ExamOne.Helper;
using ExamOne.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace ExamOne.Service
{
    public interface IAccountService
    {
        Task<ResponderData<string>> Login(AccountModel model);
        Task<ResponderData<string>> Register(AccountModel model);
        Task<ResponderData<string>> Logout();
        Task<ResponderData<BranchModel>> GetBranches();
        Task<ResponderData<string>> UpdateProfile(ProfileModel model);
        Task<ResponderData<string>> ChangePassword(PasswordModel model);
    }
    public class AccountService : IAccountService
    {
        private readonly ExamOneDbContext _examOneDbContext;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly string defaultAvatarLink = "/img/user.jpg";
        private IImageManagement _imageManagement;
        public AccountService(ExamOneDbContext examOneDbContext,
            UserManager<Account> userManager,SignInManager<Account> signInManager, IImageManagement imageManagement)
        {
            _examOneDbContext = examOneDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _imageManagement = imageManagement;
        }
        public async Task<ResponderData<string>> Login(AccountModel model)
        {
            var responder = new ResponderData<string>();
            if(model == null)
            {
                responder.Message = "Thông tin không hợp lệ";
                return responder;
            }
            if (string.IsNullOrEmpty(model.Username))
            {
                responder.Message = "Tên đăng nhập không được để trống";
                return responder;
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                responder.Message = "Mật khẩu không được để trống";
                return responder;
            }

            var user = await _userManager.FindByNameAsync(model.Username)
                 ?? await _userManager.FindByEmailAsync(model.Username);

            if(user == null)
            {
                responder.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
                return responder;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                responder.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
                return responder;
            }

            var random = DateTime.Now.Ticks;
            var claims = new List<Claim>
            {
                new Claim("FullName", user.FullName),
                new Claim("Email", user.Email),
                new Claim("BranchCode", user.BranchCode),
                new Claim("Avatar", $"{user.Avatar}?v={random}" ?? defaultAvatarLink)

            };
            await _signInManager.SignInWithClaimsAsync(user, isPersistent: model.RememberMe, claims);

            responder.IsSuccess = true;
            return responder;
        }

        public async Task<ResponderData<string>> Logout()
        {

            await _signInManager.SignOutAsync();
            return new ResponderData<string> { IsSuccess = true , Message = "Đăng xuất thành công" };
        }

        public async Task<ResponderData<string>> Register(AccountModel model)
        {
            var responder = ValidatorData.CheckRequiredFields(model);
            if (!responder.IsSuccess)
            {
                return responder;
            }
            responder.IsSuccess = false;

            if(model.Password != model.PasswordConfirm)
            {
                responder.Message = "Mật khẩu phải trùng khớp";
                return responder;
            }

            var userValidate = await _userManager.FindByNameAsync(model.Username);
            if(userValidate != null)
            {
                responder.Message = "Tài khoản đã tồn tại";
                return responder;
            }

            userValidate = await _userManager.FindByEmailAsync(model.Email);
            if (userValidate != null)
            {
                responder.Message = "Email đã được sử dụng";
                return responder;
            }

            userValidate = await _userManager.Users.FirstOrDefaultAsync(c => c.CCCD == model.CCCD);
            if (userValidate != null)
            {
                responder.Message = "CCCD đã được sử dụng";
                return responder;
            }

            var user = new Account
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                CCCD = model.CCCD,
                ProvinceCode = model.ProvinceCode,
                WardCode = model.WardCode,
                BranchCode = model.BranchCode
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var random = DateTime.Now.Ticks;
                await _userManager.AddToRoleAsync(user, "User");
                var claims = new List<Claim>
                {
                    new Claim("FullName", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("BranchCode", user.BranchCode),
                    new Claim("Avatar", $"{user.Avatar}?v={random}")

                };
                await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);
                //await _signInManager.SignInAsync(user, isPersistent: false);
                responder.IsSuccess = true;
                return responder;
            }
            responder.Message = "Tạo tài khoản không thành công";
            return responder;
        }

        public async Task<ResponderData<BranchModel>> GetBranches()
        {
            var result = new ResponderData<BranchModel>();
            result.DataList = await _examOneDbContext.Branches
                                .Select(c => new BranchModel
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                }).ToListAsync();
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResponderData<string>> UpdateProfile(ProfileModel model)
        {
            var result = new ResponderData<string>();
            if (model == null || string.IsNullOrEmpty(model.UserName)) {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }

            if (string.IsNullOrEmpty(model.Avatar))
            {
                result.Message = "Không có dữ liệu ảnh tải lên";
                return result;
            }

            if (string.IsNullOrEmpty(model.FullName))
            {
                result.Message = "Ho tên không được để trống";
                return result;
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }

            var avatarLink = user.Avatar;
            if(!model.Avatar.Contains("http"))
            {
                var resultAvatar = await _imageManagement.UpdateAvatar(model.UserName, model.Avatar);
                if (!resultAvatar.IsSuccess)
                {
                    result = resultAvatar;
                    return result;
                }
                avatarLink = resultAvatar.Data;
            }

            user.Avatar = avatarLink;
            user.FullName = model.FullName;
            var resultUser = await _userManager.UpdateAsync(user);
            if (resultUser.Succeeded)
            {
                var random = DateTime.Now.Ticks;
                var claims = new List<Claim>
                {
                    new Claim("FullName", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("BranchCode", user.BranchCode),
                    new Claim("Avatar", $"{user.Avatar}?v={random}")

                };
                await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);
                //await _signInManager.SignInAsync(user, isPersistent: false);
                result.IsSuccess = true;
                result.Message = "Cập nhật thành công";
                return result;
            }
            result.Message = "Cập nhật lỗi";
            return result;
        }

        public async Task<ResponderData<string>> ChangePassword(PasswordModel model)
        {
            var result = new ResponderData<string>();
            if(model == null)
            {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }
            var data = ValidatorData.CheckRequiredFields(model);
            if (!data.IsSuccess)
            {
                result.Message = data.Message;
                return result;
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                result.Message = "Mật khẩu xác nhận không khớp";
                return result;
            }

            var user = await _userManager.FindByNameAsync(model.CreateBy);
            if (user == null) {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }
            var validatePassword = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if (!validatePassword)
            {
                result.Message = "Mật khẩu hiện tại không chính xác";
                return result;
            }
            var resultUser = await _userManager.ChangePasswordAsync(
                        user,
                        model.OldPassword,
                        model.NewPassword
                    );

            if (resultUser.Succeeded)
            {
                var random = DateTime.Now.Ticks;
                var claims = new List<Claim>
                {
                    new Claim("FullName", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("BranchCode", user.BranchCode),
                    new Claim("Avatar", $"{user.Avatar}?v={random}")

                };
                await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);
                //await _signInManager.SignInAsync(user, isPersistent: false);
                result.IsSuccess = true;
                result.Message = "Cập nhật thành công";
                return result;
            }
            result.Message = "Cập nhật lỗi";
            return result;
        }
    }
}
