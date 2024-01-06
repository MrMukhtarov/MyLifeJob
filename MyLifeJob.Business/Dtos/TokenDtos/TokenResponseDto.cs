﻿namespace MyLifeJob.Business.Dtos.TokenDtos;

public class TokenResponseDto
{
    public string Token { get; set; }
    public string Username { get; set; }
    public DateTime Expires { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
    public List<string> Roles { get; set; }

    public TokenResponseDto()
    {
        Roles = new List<string>();
    }
}
