using System.ComponentModel;

namespace TestWebApi.Enums;

public enum SchedulerJobType
{
    [Description("2주치 api로그 삭제")]
    deleteApiLogs,

    [Description("만료된 리프레쉬토큰 삭제")]
    deleteRefreshTokens,

    [Description("2주가 지난 Exception Log 삭제")]
    deleteExpiredExceptionLogs
}
