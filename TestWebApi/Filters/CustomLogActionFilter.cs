using System.Diagnostics;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TestWebApi.Services.Logging.ApiLogService;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TestWebApi.Filters;

public class CustomLogActionFilter : IAsyncActionFilter
{
    private readonly ILog _logger;
    private readonly IApiLogService _apiLogService;

    public CustomLogActionFilter(ILog logger, IApiLogService apiLogService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _apiLogService = apiLogService;
    }


    /// <summary>
    /// Controller의 Request와 Response때 로그 발생시킴
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param> 
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        /* On Request */


        var controller = (string)context.RouteData.Values["Controller"]!;
        var action = (string)context.RouteData.Values["action"]!;
        var headerId = context.HttpContext.Request?.Headers["userId"].ToString();
        var headerProgramNm = context.HttpContext.Request?.Headers["programNm"].ToString();
        var headerAuthorization = context.HttpContext.Request?.Headers["Authorization"].ToString();


        var headerValue = JsonConvert.SerializeObject(context.HttpContext.Request?.Headers);

        var reqContents = JsonSerializer.Serialize(context.ActionArguments);

        var method = context.HttpContext.Request?.Method;
        var path = context.HttpContext.Request?.Path.ToString();

        var stopWatch = new Stopwatch();


        stopWatch.Start();
        var reqLogNo = await _apiLogService.InsertLogAsync(new ApiLog()
        {
            reqContents = reqContents,
            controllerName = controller,
            userId = headerId,
            action = action,
            method = method,
            path = path,
            headerValue = headerValue ?? "",
        });

        /*여기서 컨트롤러 비즈니스 로직 실행됨*/
        var resultContext = await next();

        stopWatch.Stop();

        int? httpResult = null;
        var resContent = string.Empty;
        switch (resultContext.Result)
        {
            /* On Response */
            case ObjectResult objResult:
                httpResult = objResult.StatusCode;
                resContent = JsonConvert.SerializeObject(objResult.Value,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                break;
            case UnauthorizedResult unauthorizedResult:
                httpResult = unauthorizedResult.StatusCode;
                break;
            case StatusCodeResult statusCodeResult:
                httpResult = statusCodeResult.StatusCode;
                break;
        }

        if (resultContext.Exception != null)
            httpResult = 500;


        await _apiLogService.UpdateLogAsync(new ApiLog()
        {
            logNo = reqLogNo,
            resContents = resContent,
            statusCode = httpResult,
            elapsedSec = stopWatch.Elapsed,
        });
    }
}