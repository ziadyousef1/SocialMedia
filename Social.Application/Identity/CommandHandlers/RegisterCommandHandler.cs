using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Social.Application.Enums;
using Social.Application.Identity.Commands;
using Social.Application.Models;
using Social.Application.Options;
using Social.Application.Services;
using Social.DAL;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.Identity.CommandHandlers;

public class RegisterCommandHandler:IRequestHandler<RegisterCommand, OperationResult<string>>
{
    private readonly DataContext _dataContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IdentityService _identityService;
    public RegisterCommandHandler(DataContext dataContext, UserManager<IdentityUser> userManager,
    IOptions<JwtSettings> jwtSettings, IdentityService identityService)
    {
    
        _dataContext = dataContext;
        _userManager = userManager;
        _identityService = identityService;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<OperationResult<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<string>();
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Username);
            if (existingUser != null)
            {
                operationResult.IsSuccess = false;
                var error = new Error
                {
                    Code = ErrorCode.UserAlreadyExists,
                    Message = "User with this email already exists."
                };
                operationResult.Errors.Add(error);
                return operationResult;
            }
            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Username,
                PhoneNumber = request.PhoneNumber
            };

            await using var transaction = await _dataContext.Database.BeginTransactionAsync(cancellationToken);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                operationResult.IsSuccess = false;
                foreach (var error in result.Errors)
                {
                    operationResult.Errors.Add(new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = error.Description
                    });
                }
                return operationResult;
            }
               
            var profileInfo = BasicInfo.Create(request.FirstName, request.LastName, request.Username,
                    request.PhoneNumber, request.DateOfBirth, request.CurrentCity);
             var profile = UserProfile.Create(user.Id, profileInfo);
            try
            {
               
                _dataContext.UserProfiles.Add(profile);
                await _dataContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            var claims = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserProfileId", profile.UserProfileId.ToString())
                });
          var token = _identityService.GenerateToken(claims.Claims);
              
            operationResult.IsSuccess = true;
            operationResult.Payload = token;

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