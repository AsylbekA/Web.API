using Auth.Application;
using Auth.Application.Cache.Redis;
using Auth.Application.Cache.Redis.Interfaces;
using Auth.Application.Helper;
using Auth.Infrastructure;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProductPersistenceApplication();
builder.Services.AddProductPersistence(builder.Configuration);
builder.Services.AddControllers();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<ICacheService, CacheService>();
#region Swagger

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Auth.API",
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x => x
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    #region Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth.API");
    });
    #endregion
}

app.Run();
