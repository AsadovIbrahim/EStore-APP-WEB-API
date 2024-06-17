using EStore.Persistance.Contexts;
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
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services,IConfiguration configuration) {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("default"));
            });


            return services;
        }
    }
}
