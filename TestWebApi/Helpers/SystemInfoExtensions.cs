
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

        var programNm = request.Headers["programNm"].ToString();
        var userId = request.Headers["userId"].ToString();
        programNm = string.IsNullOrEmpty(programNm) ? "unidentified" : programNm;
        userId = string.IsNullOrEmpty(userId) ? "unidentified" : userId;
        var ipAddress = _httpContextExtensions.GetIp();

        var isInsert = string.IsNullOrEmpty(model.createdBy);
        model.updatedAt = DateTime.Now;
        model.updatedBy = programNm;
        model.updatedId = userId;
        model.updatedIp = ipAddress;

        if (!isInsert)
            return;
        model.createdAt = DateTime.Now;
        model.createdBy = programNm;
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

        var programNm = "system";
        var userId = "system";
        var ipAddress = _httpContextExtensions.GetIp();

        var isInsert = string.IsNullOrEmpty(model.createdBy);
        model.updatedAt = DateTime.Now;
        model.updatedBy = programNm;
        model.updatedId = userId;
        model.updatedIp = ipAddress;
        if (!isInsert)
            return;
        model.createdAt = DateTime.Now;
        model.createdBy = programNm;
        model.createdId = userId;
        model.createdIp = ipAddress;
    }
}