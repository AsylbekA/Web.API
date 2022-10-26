using Auth.Application.Helper;
using Auth.Application.Models;
using Auth.Application.Models.Interfaces;
using Auth.Application.Services.Interfaces;
using Auth.Application.Services;
using FluentAssertions.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.Cache.Redis.Interfaces;
using Auth.Application.Cache.Redis;

namespace Auth.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductPersistenceApplication(this IServiceCollection services)
        {
            //services.AddScoped<IProductService, ProductServiceImp>();
            services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IAuthenticateRequest, AuthenticateRequestImp>();
            // services.AddScoped<IAuthenticateResponse, AuthenticateResponseImp>();
            services.AddScoped<ICacheService, CacheService>();
            return services;
        }
    }
}
