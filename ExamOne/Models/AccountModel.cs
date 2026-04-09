using System.ComponentModel.DataAnnotations;

namespace ExamOne.Models
{
    public class AccountModel
    {
        [Display(Name = "Tên đăng nhập")]
        public string? Username { get; set; }
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }
        [Display(Name = "Mật khẩu xác nhận")]
        public string? PasswordConfirm { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Họ tên")]
        public string? FullName { get; set; }
        [Display(Name = "Căn cước")]
        public string? CCCD { get; set; }
        [Display(Name = "Tỉnh/thành phố")]
        public string? ProvinceCode { get; set; } = "31"; // thanh pho Hai Phong
        [Display(Name = "Xã/phường")]
        public string? WardCode { get; set; } = "11602"; // phuong Hong An
        [Display(Name = "Chi đoàn")]
        public string? BranchCode { get; set; }
        public bool RememberMe { get; set; }
        public bool AcceptTerms { get; set; }
    }
}
