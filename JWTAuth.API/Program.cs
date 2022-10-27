using JWTAuth.API.Middleware;
using JWTAuth.Application.Interfaces;
using JWTAuth.Application.User.Login;
using JWTAuth.Domain.Persistence;
using JWTAuth.EFData;
using JWTAuth.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJTWAuthPersistence(builder.Configuration);
builder.Services.AddMediatR(typeof(LoginHandler).Assembly);

builder.Services.AddMvc(option =>
{
    option.EnableEndpointRouting = false;
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    option.Filters.Add(new AuthorizeFilter(policy));
}).SetCompatibilityVersion(CompatibilityVersion.Latest);

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

var buil = builder.Services.AddIdentityCore<AppUser>();
var identityBuilder = new IdentityBuilder(buil.UserType, buil.Services);
identityBuilder.AddEntityFrameworkStores<JWTAuthContextImp>();
identityBuilder.AddSignInManager<SignInManager<AppUser>>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("vQsdhTbb$33457676gbgfbj0_fvds0561!!dc&nHFDYfRJ"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false,
        };
    });

builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWTAuth.API",
    });
});
#endregion

var app = builder.Build();




using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<JWTAuthContextImp>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        context.Database.Migrate();
    }
    catch (Exception ex) { }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    #region Swagger
    app.UseSwaggerUI();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTAuth.API");
    });
    #endregion
}





app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseMvcWithDefaultRoute();
app.MapControllers();

app.Run();
