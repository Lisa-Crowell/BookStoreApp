using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BookStoreApp.API.Configurations;
using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("BookStoreDbConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options => 
    options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

builder.Services.AddAutoMapper(typeof(MapperConfig));

// var configuration = new ConfigurationBuilder()
//     .AddEnvironmentVariables()
//     .AddCommandLine(args)
//     .AddJsonFile("appsettings.json")
//     .AddUserSecrets<Program>(true)
//     .Build();

builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookStoreDbContext>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// this takes over the logging for the application, remove the logging feature from appsettings.json
// and appsettings.Development.json when you use this
builder.Host.UseSerilog((ctx, logconf) => logconf.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();