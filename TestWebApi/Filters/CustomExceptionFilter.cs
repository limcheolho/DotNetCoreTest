using Microsoft.AspNetCore.Mvc.Filters;

namespace TestWebApi.Filters;

public class CustomExceptionFilter:IAsyncExceptionFilter
{
    
    private readonly ExceptionLogHelper _exceptionLogHelper;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IExceptionLogService _exceptionLogService;

    public CustomExceptionFilter(
        ExceptionLogHelper exceptionLogHelper,
        IExceptionLogService exceptionLogService,
        IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
        _exceptionLogHelper = exceptionLogHelper;
        _exceptionLogService = exceptionLogService;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        //https: //stackoverflow.com/questions/2770303/how-to-find-in-which-controller-action-an-error-occurred
        var controllerName = context.RouteData.Values["controller"]?.ToString();
        var actionName = context.RouteData.Values["action"]?.ToString();
        // if (!_hostEnvironment.IsDevelopment())
        // {
        //     // Don't display exception details unless running in Development.
        //     return;
        // }

        _exceptionLogHelper.ControllerLog(context.Exception, controllerName ?? string.Empty, actionName ?? string.Empty);

        var model = new ExceptionLog()
        {
            controllerName = controllerName,
            actionName = actionName,
            message = context.Exception.Message,
            stackTrace = context.Exception.StackTrace,
            createdAt = DateTime.Now,
        };
        await _exceptionLogService.InsertControllerExceptionLogAsync(model);
    }
}