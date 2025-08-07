using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.UserProfiles.Queries;
using Social.DAL;
using Social.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social.Application.Models;
using Social.Application.Enums;

namespace Social.Application.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById, OperationResult<UserProfile>>
    {
        private readonly DataContext _context;

        public GetUserProfileByIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<UserProfile>> Handle(GetUserProfileById request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<UserProfile>();
            try
            {
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up=>up.UserProfileId == request.UserProfileId);
                if (userProfile == null)
                {
                    operationResult.IsSuccess = false;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"User profile with id {request.UserProfileId} not found."
                    };
                    operationResult.Errors.Add(error);
                    return operationResult;
                }
                
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
