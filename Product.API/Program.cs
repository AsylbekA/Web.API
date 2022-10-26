using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using Product.Application;
using Product.Application.Cache.Redis;
using Product.Application.Cache.Redis.Interfaces;
using Product.Application.Features.Behaviours;
using Product.Infrastructure;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = AppDomain.CurrentDomain.Load("Product.Application");
builder.Services.AddMediatR(assembly);
builder.Services.AddProductPersistence(builder.Configuration);
builder.Services.AddProductPersistenceApplication();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CleanArchitecture.API",
    });
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    #region Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture.API");
    });
    #endregion
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
