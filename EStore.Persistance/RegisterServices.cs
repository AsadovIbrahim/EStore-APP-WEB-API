using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Persistance.Contexts;
using EStore.Persistance.Repositories.Concretes;
using EStore.Persistance.Services.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance
{
    public static class RegisterServices
    {
        public static void AddPersistanceServices(this IServiceCollection services,IConfiguration configuration) {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                       .UseSqlServer(configuration.GetConnectionString("default"));
            });

            //Repository Registers

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();


            //Service Registers

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
