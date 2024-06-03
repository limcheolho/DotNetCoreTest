using TestWebApi.Services.AuthService;
using TestWebApi.Services.Logging.ApiLogService;
using TestWebApi.Services.Logging.SchedulerLogService;
using TestWebApi.Services.TelegramService;
using TestWebApi.Services.TodoService;

namespace TestWebApi.Startup;

public static class DependencyInjectionSetup
{
    public static void RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<JwtSettings>(configuration.GetSection("JWT"));

        //인증
        services.AddScoped<IAuthService, AuthService>();

        //todo
        services.AddScoped<ITodoService, TodoService>();

        //api로그
        services.AddScoped<IApiLogService, ApiLogService>();

        //exception로그
        services.AddScoped<IExceptionLogService, ExceptionLogService>();
        //schedulerbase
        services.AddScoped<SchedulerBase>();

        //스케쥴러 로그
        services.AddScoped<ISchedulerLogService, SchedulerLogService>();
        //jwtHelper
        services.AddSingleton<JwtHelper>();

        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<HttpContextExtensions>();
        services.AddSingleton<SystemInfoExtensions>();
        services.AddSingleton<ExceptionLogHelper>();

        //Telegram
        services.AddSingleton<ITelegramBotClient>(
            new TelegramBotClient(configuration["Telegram:ApiKey"]!)
        );
        services.AddSingleton<ITelegramService, TelegramService>();
    }
}