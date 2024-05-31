namespace TestWebApi.DataContext.Models;

public class Todo:TableBase
{
    public int todoNo { get; set; }
    public required string userId { get; set; }
    public required string title { get; set; }
    public string? contents { get; set; }
    public User? user { get; set; }

}