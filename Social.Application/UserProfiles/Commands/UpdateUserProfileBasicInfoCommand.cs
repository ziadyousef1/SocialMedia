using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.Commands;

public class UpdateUserProfileBasicInfoCommand :IRequest<OperationResult<UserProfile>>
{
    public Guid UserProfileId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CurrentCity { get; set; }
    
}