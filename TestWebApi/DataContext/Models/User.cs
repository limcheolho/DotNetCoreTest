using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApi.DataContext.Models;

public class User : TableBase
{
    public required string userId { get; set; }
    public required string userName { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    [NotMapped] public string? passwordAgain { get; set; }

    public string? phoneNumber { get; set; }
    public string? zipCode { get; set; }
    public string? address1 { get; set; }
    public string? address2 { get; set; }

    public int totalTodos { get; set; }

    public IList<RefreshToken>? refreshTokens { get; set; }
    public IList<Todo>? todos { get; set; }
    
    
    
}