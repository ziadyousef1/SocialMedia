using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Social.Domain.Aggregates.UserProfileAggregate;
using System.Threading.Tasks;

namespace Social.Application.UserProfiles.Commands
{
    public class CreateUserCommand : IRequest<UserProfile>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; }
    }
}
