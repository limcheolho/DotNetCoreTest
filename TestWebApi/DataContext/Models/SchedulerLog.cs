namespace TestWebApi.DataContext.Models;

public class SchedulerLog : TableBase
{
    public int jobNo { get; set; }

    //deleteLog
    //regularConsultingKakaoTalk
    public string jobType { get; set; }

    //success
    //failed
    public string? resultType { get; set; }
    public string? remarks { get; set; }
}
