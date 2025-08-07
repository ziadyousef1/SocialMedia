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
using Social.Application.Models;
using Social.Application.Enums;

namespace Social.Application.UserProfiles.CommandHandlers
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OperationResult<UserProfile>>
    {
     
        private readonly DataContext _context;
   

 

        public CreateUserCommandHandler(DataContext context)
        {
            _context = context;

        }

        public async Task<OperationResult<UserProfile>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<UserProfile>();
            try
            {
                var basicInfo = BasicInfo.Create(request.FirstName, request.LastName, request.Email
                    , request.PhoneNumber, request.DateOfBirth, request.CurrentCity);
                var userProfile = UserProfile.Create(Guid.NewGuid().ToString(),basicInfo);
                _context.UserProfiles.Add(userProfile);
                await _context.SaveChangesAsync(cancellationToken);
                operationResult.Payload = userProfile;
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    Code = ErrorCode.ServerError,
                    Message = e.Message
                };
                operationResult.IsSuccess = false;
                operationResult.Errors.Add(error);
            }
            
            return operationResult;


        }
    }
}
