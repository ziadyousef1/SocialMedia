using MediatR;
using Social.Application.Models;

namespace Social.Application.Identity.Commands;

public class RegisterCommand:IRequest<OperationResult<string>>
{
   
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set;  }
    public string LastName { get; set;  }
    public DateTime DateOfBirth { get; set;  }
    public string CurrentCity { get; set;  }
    public string PhoneNumber { get; private set; }
}