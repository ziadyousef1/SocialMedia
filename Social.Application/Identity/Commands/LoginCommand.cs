using System.ComponentModel.DataAnnotations;
using MediatR;
using Social.Application.Models;

namespace Social.Application.Identity.Commands;

public class LoginCommand: IRequest<OperationResult<string>>
{
    [Required]
    [EmailAddress]
    public string Username { get; set; } 
    [Required]
    public string Password { get; set; }
}