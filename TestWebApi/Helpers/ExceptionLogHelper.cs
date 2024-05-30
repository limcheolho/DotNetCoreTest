using System.Diagnostics;
using System.Runtime.CompilerServices;
using log4net;

namespace TestWebApi.Helpers;

public class ExceptionLogHelper
{
    private readonly ILog _logger;


    public ExceptionLogHelper(ILog logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    public void ControllerLog(Exception inner, string controllerName, string actionName)
    {
        _logger.Fatal(
            $"\n\n[ERR]:\nControllerName : {controllerName}\nActionName : {actionName}\nError : \n{inner}");
        // WriteControllerLogAsync(controllerName, actionName, inner.Message, inner.StackTrace);
    }


    public void Log(Exception inner, [CallerMemberName] string callerName = null)
    {
        var methodInfo = new StackTrace().GetFrame(1)!.GetMethod();
        // var className = methodInfo.ReflectedType.Name;
        var className = methodInfo!.ReflectedType!.ReflectedType!.UnderlyingSystemType.ToString();
        Console.WriteLine(inner.ToString());
        _logger.Fatal($"\n\n[ERR]:\nClassName : {className}\nMethodName : {callerName}\nError : \n{inner}");
    }
}