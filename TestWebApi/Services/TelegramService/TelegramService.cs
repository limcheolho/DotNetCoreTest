
namespace TestWebApi.Services.TelegramService;


public class TelegramService : ITelegramService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IConfiguration _configuration;

    public TelegramService(ITelegramBotClient botClient, IConfiguration configuration)
    {
        _botClient = botClient;
        _configuration = configuration;
    }

    public async Task SendTextAsync(string text)
    {
        await _botClient.SendTextMessageAsync(
            chatId: _configuration["Telegram:ChatId"]!,
            text: text
        );
    }

    public async Task SendSchedulerStartAsync(SchedulerJobType jobType, int jobNo)
    {
        var jobTypeName = jobType.ToDescription();
        string format = $"[💍{jobTypeName} 배치 시작👓]\n\n" + $"jobNo : {jobNo}";
        await SendTextAsync(format);
    }

    public async Task SendSchedulerTerminatedAsync(
        SchedulerJobType jobType,
        int jobNo,
        string message
    )
    {
        var jobTypeName = jobType.ToDescription();
        string format =
            $"[👓{jobTypeName} 배치 성공👓]\n\n" + $"jobNo : {jobNo}\n" + $"message : {message}";
        await SendTextAsync(format);
    }

    public async Task SendExceptionAsync(ExceptionLog exception)
    {
        string format =
            $"[✨에러발생✨]\n\n"
            + $"logNo:{exception.logNo}\n"
            + $"controllerName:{exception.controllerName}\n"
            + $"actionName:{exception.actionName}\n"
            + $"methodName:{exception.methodName}\n"
            + $"createdIp:{exception.createdIp}\n"
            + $"createdBy:{exception.createdBy}\n\n\n"
            + $"{exception.message}\n\n"
            + $"{exception.stackTrace}";

        // 문자열을 4000자씩 잘라서 처리
        for (int i = 0; i < format.Length; i += 4000)
        {
            var messagePart = format.Substring(i, Math.Min(4000, format.Length - i));
            await SendTextAsync(messagePart);
        }
    }
}