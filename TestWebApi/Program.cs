using Microsoft.EntityFrameworkCore;
using TestWebApi.DataContext;
using TestWebApi.Filters;
using TestWebApi.Startup;

var builder = WebApplication.CreateBuilder(args);
builder
    .Services.AddControllers(options =>
    {
        options.Filters.Add<CustomLogActionFilter>();
        options.Filters.Add<CustomExceptionFilter>();
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


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

app.Run();

