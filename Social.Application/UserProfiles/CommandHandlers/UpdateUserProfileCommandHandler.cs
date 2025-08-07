using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.Enums;
using Social.Application.Models;
using Social.Application.UserProfiles.Commands;
using Social.DAL;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.CommandHandlers;

public class UpdateUserProfileCommandHandler: IRequestHandler<UpdateUserProfileBasicInfoCommand, OperationResult<UserProfile>>
{
    private readonly DataContext _context;
    public UpdateUserProfileCommandHandler(DataContext context)
    {
        _context = context;
    }
    public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileBasicInfoCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<UserProfile>();
        try
        {
            var userProfile =
                await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);
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

            var basicInfo = BasicInfo.Create(request.FirstName, request.LastName, request.Email
                , request.PhoneNumber, request.DateOfBirth, request.CurrentCity);
            userProfile.UpdateBasicInfo(basicInfo);
            _context.UserProfiles.Update(userProfile);
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