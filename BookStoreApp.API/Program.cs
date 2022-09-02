using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("BookStoreDbConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options => 
    options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// this takes over the logging for the application, remove the logging feature from appsettings.json
// and appsettings.Development.json when you use this
builder.Host.UseSerilog((ctx, logconf) => logconf.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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

app.UseAuthorization();

app.MapControllers();

app.Run();