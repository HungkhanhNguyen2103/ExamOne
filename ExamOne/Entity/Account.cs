using Microsoft.AspNetCore.Identity;

namespace ExamOne.Entity
{
    public class Account : IdentityUser
    {
        public string? FullName { get; set; }
        public string? CCCD { get; set; }
        public string? ProvinceCode { get; set; } = "31"; // thanh pho Hai Phong
        public string? WardCode { get; set; } = "11602"; // phuong Hong An
        public string? BranchCode { get; set; }
        public string? Avatar { get; set; } = "/img/user.jpg";
    }
}
