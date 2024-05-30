namespace TestWebApi.DataContext.Models;

public class Todo:BaseBase
{
    public int todoNo { get; set; }
    public string userId { get; set; }
    public string title { get; set; }
    public string contents { get; set; }
    public User? user { get; set; }

}