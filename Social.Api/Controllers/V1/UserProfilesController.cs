using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.UserProfile.Requests;
using Social.Api.Contracts.UserProfile.Responses;
using Social.Application.UserProfiles.Commands;
using Social.Application.UserProfiles.Queries;
using Social.Domain.Aggregates.UserProfileAggregate;
using System.Text;
using Social.Application.Common;
using Social.Application.Enums;

namespace Social.Api.Controllers.V1
{
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfilesController:BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserProfilesController(IMediator mediator,IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles( )
        {
           var query= new GellAllUserProfilesQuery();
            var response = await _mediator.Send(query);
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
            
            var profiles = _mapper.Map<List<UserProfile>>(response.Payload);
            return Ok(profiles);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreateOrUpdate Profile)
        {

            var command = _mapper.Map<CreateUserCommand>(Profile);
            var response =await _mediator.Send(command);
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
            
            var userProfile= _mapper.Map<UserProfileResponse>(response.Payload);
            return CreatedAtAction(nameof(GetUserProfileById),new {id=response.Payload.UserProfileId},userProfile);

        }
        [HttpGet]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var query = new GetUserProfileById { UserProfileId= Guid.Parse(id)};
            var response = await _mediator.Send(query);
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
            
            var userProfile = _mapper.Map<UserProfile>(response.Payload);
            return Ok(userProfile);
        }
        [HttpPatch]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UserProfileCreateOrUpdate userProfileUpdate)
        {
            var command = _mapper.Map<UpdateUserProfileBasicInfoCommand>(userProfileUpdate);
            command.UserProfileId = Guid.Parse(id);
            var response = await _mediator.Send(command);
            return !response.IsSuccess ? HandleErrorResponse(response.Errors) : NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var command = new DeleteUserProfileCommand { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(command);
            return !response.IsSuccess ? HandleErrorResponse(response.Errors) : NoContent();
        }
    }
}
