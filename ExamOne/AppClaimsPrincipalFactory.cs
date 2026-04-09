using ExamOne.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExamOne
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<Account, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<Account> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Account user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if (!string.IsNullOrEmpty(user.FullName))
            {
                identity.AddClaim(new Claim("FullName", user.FullName));
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                identity.AddClaim(new Claim("Email", user.Email));
            }

            if (!string.IsNullOrEmpty(user.BranchCode))
            {
                identity.AddClaim(new Claim("BranchCode", user.BranchCode));
            }

            if (!string.IsNullOrEmpty(user.Avatar))
            {
                string avatarUrl = user.Avatar;

                if (!string.IsNullOrEmpty(avatarUrl) && !avatarUrl.Contains("?v="))
                {
                    avatarUrl += $"?v={DateTime.Now.Ticks}";
                }

                identity.AddClaim(new Claim("Avatar", avatarUrl));
            }

            return identity;
        }
    }

}
