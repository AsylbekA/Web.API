using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.EFData
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJTWAuthPersistence(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<JWTAuthContextImp>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(JWTAuthContextImp).Assembly.FullName)), ServiceLifetime.Transient);

        //    services.AddScoped<JWTAuthContextImp, JWTAuthContextImp>();
            return services;
        }
    }
}
