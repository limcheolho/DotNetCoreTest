﻿using TestWebApi.Services;
using TestWebApi.Services.AuthService;
using TestWebApi.Services.Logging.ApiLogService;
using TestWebApi.Services.Logging.SchedulerLogService;
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

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IApiLogService, ApiLogService>();
        services.AddScoped<IExceptionLogService, ExceptionLogService>();
        services.AddScoped<SchedulerBase>();
        services.AddScoped<ISchedulerLogService, SchedulerLogService>();
        
        services.AddSingleton<JwtHelper>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<HttpContextExtensions>();
        services.AddSingleton<SystemInfoExtensions>();
        services.AddSingleton<ExceptionLogHelper>();
    }
}
