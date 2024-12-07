using AutoMapper;
using MediatR;
using Social.Application.UserProfiles.Commands;
using Social.DAL;
using Social.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.UserProfiles.CommandHandlers
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserProfile>
    {
     
        private readonly DataContext _context;
   

 

        public CreateUserCommandHandler(DataContext context)
        {
            _context = context;

        }

        public async Task<UserProfile> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
         
            var basicInfo = BasicInfo.Create(request.FirstName, request.LastName, request.Email
                , request.PhoneNumber, request.DateOfBirth, request.CurrentCity);
            var userProfile = UserProfile.Create(Guid.NewGuid().ToString(),basicInfo);
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return userProfile; 


        }
    }
}
