using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using TestWebApi.Filters;
using TestWebApi.Services.TelegramService;
using TestWebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    {
        //filter 등록
        options.Filters.Add<CustomLogActionFilter>();
        options.Filters.Add<CustomExceptionFilter>();
    })
    //json loop 방지
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

//log4net 설정
builder.Services.AddLog4net();
//jwt 설정
builder.Services.JwtStartup(builder.Configuration);

//swagger 쓴다고 설정
builder.Services.SwaggerStartup();

//DI 주입
builder.Services.RegisterServices(builder.Configuration);

//Quartz 스케쥴러 실행
builder.Services.QuartzStartup();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB 접속 설정
var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
builder.Services.AddDbContext<TestDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

//jwt 인증 사용
app.UseAuthorization();

//컨트롤러 맵핑
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


//서비스 시작시 텔레그램으로 메시지 보내기.
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