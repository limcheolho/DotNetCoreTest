using Quartz;
using TestWebApi.Schedulers;
using TestWebApi.Services.AuthService;

namespace TestWebApi;

public class DeleteUnusedRefreshTokensJob : IJob
{
    private readonly SchedulerBase _schedulerBase;
    private readonly IAuthService _authService;

    public DeleteUnusedRefreshTokensJob(
        SchedulerBase schedulerBase, IAuthService authService)
    {
        _schedulerBase = schedulerBase;
        _authService = authService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        async Task<string> Do()
        {
            var deleteCount = await _authService.DeleteExpiredRefreshTokenAsync();
            return $"사용하지 않는 리프레쉬토큰 삭제 총 {deleteCount}건";
        }

        await _schedulerBase.RunSchedulerAsync(SchedulerJobType.deleteRefreshTokens, Do);
    }
}