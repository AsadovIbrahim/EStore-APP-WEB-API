using EStore.Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Configurations
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasMany(p => p.UserTokens).WithOne(p => p.User).HasForeignKey(p => p.UserId);
            builder.HasOne(r => r.Role).WithMany(u => u.Users);

            builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(100);

            // Default SuperAdmin data

            using var hmac = new HMACSHA256();
            var superAdminSeedData = new User
            {
                Id = 1,
                UserName = "superadmin",
                Name = "SuperAdmin",
                Surname = "SuperAdminov",
                IsEmailConfirmed = true,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("superadmin123")),
                PasswordSalt = hmac.Key,
                Email = "superadmin@gmail.com",
                RoleId = 1
            };

            builder.HasData(superAdminSeedData);

        }
    }
}
