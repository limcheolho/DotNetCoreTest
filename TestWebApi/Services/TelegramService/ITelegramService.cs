namespace TestWebApi.Services.TelegramService;

public interface ITelegramService
{
    /// <summary>
    /// 단순 메시지 보내기
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public Task SendTextAsync(string text);

    /// <summary>
    /// quart.net 스케쥴러 잡 시작시
    /// </summary>
    /// <param name="jobType"></param>
    /// <param name="jobNo"></param>
    /// <returns></returns>
    public Task SendSchedulerStartAsync(SchedulerJobType jobType, int jobNo);

    /// <summary>
    /// quart.net 스케쥴러 잡 성공시
    /// </summary>
    /// <param name="jobType"></param>
    /// <param name="jobNo"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task SendSchedulerTerminatedAsync(SchedulerJobType jobType, int jobNo, string message);

    /// <summary>
    /// exception 발생시
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public Task SendExceptionAsync(ExceptionLog exception);
}