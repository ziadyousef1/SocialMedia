using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Identity;

public class Login
{
    [Required]
    [EmailAddress]
    public string Username { get; set; } 
    [Required]
    public string Password { get; set; } 
}