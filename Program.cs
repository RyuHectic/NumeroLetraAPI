using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NumeroLetraAPI.API_Setup;
using NumeroLetraAPI.DbContexts;
using NumeroLetraAPI.Helpers;
using NumeroLetraAPI.Repository;
using NumeroLetraAPI.Repository.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWebApi(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigins",
        builder =>
        {
            builder.AllowAnyHeader()
                           .AllowAnyOrigin()
                          .AllowAnyMethod();
        });
});

//Add DBContext for dependecy injection
var objBuilder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

IConfiguration conManager = objBuilder.Build();

var strConnection = conManager.GetConnectionString("NumberLetter");

builder.Services.AddDbContext<NumeroLetraContext>(options =>
{
    options.UseSqlServer(strConnection);
});

//Add services and interfaces for Repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<AuthHelpers>();

//Jwt configuration starts here
var jwtIssuer = conManager.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = conManager.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? ""))
     };
 });
//Jwt configuration ends here


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllOrigins");

await app.RegisterWebApisAsync();
await app.RunAsync();