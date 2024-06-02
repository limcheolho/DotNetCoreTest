namespace TestWebApi.Helpers;

/// <summary>
///
/// </summary>
/// <param name="httpContextExtensions"></param>
public class SystemInfoExtensions(HttpContextExtensions httpContextExtensions)
{
    private readonly HttpContextExtensions _httpContextExtensions = httpContextExtensions;

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public HttpRequest? GetHttpRequest() => _httpContextExtensions.GetHttpRequest();

    /// <summary>
    ///     테이블에 수정정보 기록
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    public void SetModelLogInfo<T>(T? model)
        where T : TableBase
    {
        Console.WriteLine("test", model);
        var request = _httpContextExtensions.GetHttpRequest();
        if (request == null)
        {
            SetSystemModelLogInfo(model);
            return;
        }

        if (model == null)
            return;

        var programName = request.Headers["programName"].ToString();
        var userId = request.Headers["userId"].ToString();
        programName = string.IsNullOrEmpty(programName) ? "unidentified" : programName;
        userId = string.IsNullOrEmpty(userId) ? "unidentified" : userId;
        var ipAddress = _httpContextExtensions.GetIp();

        var isInsert = string.IsNullOrEmpty(model.createdBy);
        model.updatedAt = DateTime.Now;
        model.updatedBy = programName;
        model.updatedId = userId;
        model.updatedIp = ipAddress;

        if (!isInsert)
            return;
        model.createdAt = DateTime.Now;
        model.createdBy = programName;
        model.createdId = userId;
        model.createdIp = ipAddress;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    public void SetSystemModelLogInfo<T>(T? model)
        where T : TableBase
    {
        if (model == null)
            return;

        var programName = "system";
        var userId = "system";
        var ipAddress = _httpContextExtensions.GetIp();

        var isInsert = string.IsNullOrEmpty(model.createdBy);
        model.updatedAt = DateTime.Now;
        model.updatedBy = programName;
        model.updatedId = userId;
        model.updatedIp = ipAddress;
        if (!isInsert)
            return;
        model.createdAt = DateTime.Now;
        model.createdBy = programName;
        model.createdId = userId;
        model.createdIp = ipAddress;
    }
}
