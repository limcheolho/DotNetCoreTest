﻿namespace TestWebApi.DataContext.Models;

public class RefreshToken:TableBase
{
    public required string refreshToken { get; set; }

    public required string userId { get; set; }
    public DateTime refreshTokenExpiryTime { get; set; }
    public User? user { get; set; }
}