namespace Social.Application.Options;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string[] Audience { get; set; } = Array.Empty<string>();
    public string Key { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; } = 60;
 
}