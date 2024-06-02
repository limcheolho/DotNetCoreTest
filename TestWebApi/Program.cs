using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TestWebApi.DataContext;
using TestWebApi.Filters;
using TestWebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<CustomLogActionFilter>();
        options.Filters.Add<CustomExceptionFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddLog4net();
builder.Services.JwtStartup(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.QuartzStartup();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
builder.Services.AddDbContext<TestDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
app.UseAuthorization();

app.MapControllers();

/* Cors*/
app.UseCors(x =>
{
    x.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .SetIsOriginAllowed(origin => true)
        .WithOrigins("http://*")
        .WithOrigins("https://*")
        .AllowCredentials();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();