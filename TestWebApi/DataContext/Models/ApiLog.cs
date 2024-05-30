using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestWebApi.DataContext.Models;

public class ApiLog:BaseBase
{
    public int logNo { get; set; }
    public TimeSpan? elapsedSec { get; set; }
    public string controllerName { get; set; }
    public string action { get; set; }
    public string? path { get; set; }
    public string? method { get; set; }
    public int? statusCode { get; set; }
    public string? userId { get; set; }
    public string? reqContents { get; set; }
    public string? resContents { get; set; }

} 