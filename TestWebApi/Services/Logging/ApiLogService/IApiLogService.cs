namespace TestWebApi.Services.Logging.ApiLogService;

/// <summary>
/// 로깅 및 로깅 삭제 서비스임
/// </summary>
public interface IApiLogService
{
    /// <summary>
    /// 로그 insert
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    public Task<int> InsertLogAsync(ApiLog apiLog);

    /// <summary>
    /// 로그 update
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    public Task<bool> UpdateLogAsync(ApiLog apiLog);

    /// <summary>
    /// 로그 삭제 (스케줄러 잡에서 사용)
    /// </summary>
    /// <returns></returns>
    public Task<int> DeleteLogAsync();
}
