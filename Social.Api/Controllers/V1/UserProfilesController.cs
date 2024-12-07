using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.UserProfile.Requests;
using Social.Api.Contracts.UserProfile.Responses;
using Social.Application.UserProfiles.Commands;
using Social.Application.UserProfiles.Queries;
using Social.Domain.Aggregates.UserProfileAggregate;
using System.Text;
namespace Social.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfilesController:ControllerBase
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
            var Profiles= _mapper.Map<List<UserProfile>>(response);

            return Ok(Profiles);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreate Profile)
        {

            var command = _mapper.Map<CreateUserCommand>(Profile);
            var response =await _mediator.Send(command);
            var userProfile= _mapper.Map<UserProfileResponse>(response);
            return CreatedAtAction(nameof(GetUserProfileById),new {id=response.UserProfileId},userProfile);

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var query = new GetUserProfileById { UserProfileId= Guid.Parse(id)};
            var response = await _mediator.Send(query);
            var userProfile = _mapper.Map<UserProfile>(response);
            return Ok(userProfile);
        }
    }
}
