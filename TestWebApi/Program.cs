using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using TestWebApi.Filters;
using TestWebApi.Services.TelegramService;
using TestWebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

//filter 등록
//json loop 방지
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

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    // 필요한 경우 KnownProxies 또는 KnownNetworks 설정
    // options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
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


// 서비스를 가져와서 사용합니다.
await using (var serviceScope = app.Services.CreateAsyncScope())
{
    var services = serviceScope.ServiceProvider;
    // 등록한 서비스를 가져옵니다.
    var myService = services.GetRequiredService<ITelegramService>();
    // 서비스의 메서드를 호출합니다.
    var name = Environment.MachineName;
    await myService.SendTextAsync($"service started : 📺{name}");
}

app.Run();