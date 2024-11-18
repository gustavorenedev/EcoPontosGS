using EcoPontos.Infrastructure.Context;
using EcoPontos.Infrastructure.Repositories;
using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.Reward;
using EcoPontos.Service.TaskRegister;
using EcoPontos.Service.TaskWork;
using EcoPontos.Services.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Configuração do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registro dos Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskWorkRepository, TaskWorkRepository>();
builder.Services.AddScoped<ITaskRegisterRepository, TaskRegisterRepository>();
builder.Services.AddScoped<IRewardRepository, RewardRepository>();

// Registro dos Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskWorkService, TaskWorkService>();
builder.Services.AddScoped<ITaskRegisterService, TaskRegisterService>();
builder.Services.AddScoped<IRewardService, RewardService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EcoPontos",
        Version = "v1",
        Description = "API para gerenciamento de clientes e sistema de pontuação para preservar energia."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
