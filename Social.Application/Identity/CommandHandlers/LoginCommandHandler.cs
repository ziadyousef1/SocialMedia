using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Social.Application.Enums;
using Social.Application.Identity.Commands;
using Social.Application.Models;
using Social.Application.Options;
using Social.Application.Services;
using Social.DAL;

namespace Social.Application.Identity.CommandHandlers;

public class LoginCommandHandler:IRequestHandler<LoginCommand, OperationResult<string>>
{
    private readonly DataContext _dataContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IdentityService _identityService;
    

    public LoginCommandHandler(DataContext dataContext, UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings, IdentityService identityService)
    {
        _dataContext = dataContext;
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<OperationResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<string>();
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Username);
            if (existingUser == null)
            {
                var error = new Error
                {
                    Code = ErrorCode.InvalidCredentials,
                    Message = "Invalid username or password."
                };
                operationResult.IsSuccess = false;
                operationResult.Errors.Add(error);
                return operationResult;

            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!isPasswordValid)
            {
                var error = new Error
                {
                    Code = ErrorCode.InvalidCredentials,
                    Message = "Invalid username or password."
                };
                operationResult.IsSuccess = false;
                operationResult.Errors.Add(error);
                return operationResult;
            }

            var profile = await _dataContext.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdentityId == existingUser.Id, cancellationToken);
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, existingUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id),
                new Claim(ClaimTypes.Email, existingUser.Email),
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
