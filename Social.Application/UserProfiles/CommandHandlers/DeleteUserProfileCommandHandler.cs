using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.UserProfiles.Commands;
using Social.DAL;
using Social.Application.Models;
using Social.Application.Enums;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.CommandHandlers;

public class DeleteUserProfileCommandHandler: IRequestHandler<DeleteUserProfileCommand, OperationResult<UserProfile>>
{
    private readonly DataContext _context;
    public DeleteUserProfileCommandHandler(DataContext context)
    {
        _context = context;
    }
    public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<UserProfile>();
        try
        {
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);
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

            _context.UserProfiles.Remove(userProfile);
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