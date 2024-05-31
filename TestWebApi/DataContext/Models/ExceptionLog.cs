namespace TestWebApi.DataContext.Models;

public class ExceptionLog:TableBase
{
    public int logNo { get; set; }
    public string? controllerName { get; set; }
    public string? actionName { get; set; }
    public string? methodName { get; set; }
    public string? message { get; set; }
    public string? stackTrace { get; set; }
}
