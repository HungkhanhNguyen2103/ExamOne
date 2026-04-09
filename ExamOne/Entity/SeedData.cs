using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExamOne.Entity
{
    public class SeedData
    {
        public static void SeedDataDb(ModelBuilder builder)
        {
            SeedAccountRole(builder);
            SeedBranch(builder);
        }

        private static void SeedAccountRole(ModelBuilder builder)
        {
            var adminId = Guid.NewGuid().ToString();
            var user1Id = Guid.NewGuid().ToString();
            var user2Id = Guid.NewGuid().ToString();
            var user3Id = Guid.NewGuid().ToString();
            var user4Id = Guid.NewGuid().ToString();
            var user5Id = Guid.NewGuid().ToString();
            var user6Id = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<Account>();
            var admin = new Account
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                FullName = "Admin User",
                CCCD = "123456789",
                ProvinceCode = "01",
                Avatar = "/img/user.jpg",
                WardCode = "001",
                BranchCode = "1",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

            var user1 = new Account
            {
                Id = user1Id,
                UserName = "user1",
                NormalizedUserName = "USER1",
                Email = "user1@gmail.com",
                NormalizedEmail = "USER1@GMAIL.COM",
                EmailConfirmed = true,
                FullName = "Normal User 1",
                CCCD = "987654321",
                ProvinceCode = "79",
                Avatar = "/img/user.jpg",
                BranchCode = "1",
                WardCode = "005",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            user1.PasswordHash = hasher.HashPassword(user1, "User1@123");

            var user2 = new Account
            {
                Id = user2Id,
                UserName = "user2",
                NormalizedUserName = "USER2",
                Email = "user2@gmail.com",
                NormalizedEmail = "USER2@GMAIL.COM",
                EmailConfirmed = true,
                FullName = "Normal User 2",
                CCCD = "987654326",
                ProvinceCode = "79",
                BranchCode = "1",
                Avatar = "/img/user.jpg",
                WardCode = "005",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            user2.PasswordHash = hasher.HashPassword(user2, "User2@123");


            builder.Entity<Account>().HasData(admin, user1, user2);

            var adminUserId = Guid.NewGuid().ToString();
            var normalUserId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminUserId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = normalUserId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminUserId,
                    UserId = adminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = normalUserId,
                    UserId = user1Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = normalUserId,
                    UserId = user2Id
                }
            );
        }
    
        private static void SeedBranch(ModelBuilder builder)
        {
            var branches = new List<Branch>
            {
                new Branch { Id = 1, Name = "Chi đoàn A" },
                new Branch { Id = 2, Name = "Chi đoàn B" },
                new Branch { Id = 3, Name = "Chi đoàn C"}
            };
            builder.Entity<Branch>().HasData(branches);
        }
    }
}
