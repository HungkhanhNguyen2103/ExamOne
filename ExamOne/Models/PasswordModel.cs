using System.ComponentModel.DataAnnotations;

namespace ExamOne.Models
{
    public class PasswordModel
    {
        [Display(Name = "Tài khoản")]
        public string? CreateBy { get; set; }
        [Display(Name = "Mật khẩu hiện tại")]
        public string? OldPassword { get; set; }
        [Display(Name = "Mật khẩu mới")]
        public string? NewPassword { get; set; }
        [Display(Name = "Mật khẩu xác nhận")]
        public string? ConfirmNewPassword { get; set; }
    }
}
