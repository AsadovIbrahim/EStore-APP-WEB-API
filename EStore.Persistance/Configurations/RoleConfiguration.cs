using EStore.Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            // Relations
            builder.HasMany(r => r.Users)
                   .WithOne(u => u.Role);

            // Hass Data
            builder.HasData(new Role[]
            {
            new Role(){Id=1, Name="SuperAdmin", IsDeleted=false, CreatedDate=DateTime.Now},
            new Role(){Id=2, Name="Admin", IsDeleted=false, CreatedDate=DateTime.Now},
            new Role(){Id=3, Name="Customer", IsDeleted=false, CreatedDate=DateTime.Now},
            new Role(){Id=4, Name="Cashier", IsDeleted=false, CreatedDate=DateTime.Now},
            });
        }
    }
}
