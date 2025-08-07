using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Social.Domain.Aggregates.UserProfileAggregate;
using System.Threading.Tasks;
using Social.Application.Models;

namespace Social.Application.UserProfiles.Commands
{
    public class CreateUserCommand : IRequest<OperationResult<UserProfile>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        public required string CurrentCity { get; set; }
    }
}
