using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.Posts.Requests;
using Social.Api.Contracts.Posts.Responses;
using Social.Application.PostInteractions.Commands;
using Social.Application.PostInteractions.Queries;

namespace Social.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/posts/{postId}/interactions")]
    [ApiController]
    public class PostInteractionsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostInteractionsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPostInteraction(string postId, [FromBody] PostInteractionRequest request)
        {
            if (!Guid.TryParse(postId, out var parsedPostId))
                return BadRequest("Invalid post ID format");

            var command = _mapper.Map<AddPostInteractionCommand>(request);
            command.PostId = parsedPostId;
            var response = await _mediator.Send(command);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var interactionResponse = _mapper.Map<PostInteractionResponse>(response.Payload);
            return Ok(interactionResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePostInteraction(string postId, [FromQuery] Guid userProfileId)
        {
            if (!Guid.TryParse(postId, out var parsedPostId))
                return BadRequest("Invalid post ID format");

            var command = new RemovePostInteractionCommand 
            { 
                PostId = parsedPostId,
                UserProfileId = userProfileId
            };
            var response = await _mediator.Send(command);
            
            return !response.IsSuccess ? HandleErrorResponse(response.Errors) : NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetPostInteractions(string postId)
        {
            if (!Guid.TryParse(postId, out var parsedPostId))
                return BadRequest("Invalid post ID format");

            var query = new GetPostInteractionsQuery { PostId = parsedPostId };
            var response = await _mediator.Send(query);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var interactions = _mapper.Map<List<PostInteractionResponse>>(response.Payload);
            return Ok(interactions);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetPostInteractionsSummary(string postId)
        {
            if (!Guid.TryParse(postId, out var parsedPostId))
                return BadRequest("Invalid post ID format");

            var query = new GetPostInteractionsQuery { PostId = parsedPostId };
            var response = await _mediator.Send(query);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);

            var interactionSummary = response.Payload?
                .GroupBy(i => i.InteractionType)
                .ToDictionary(g => g.Key.ToString(), g => g.Count()) ?? new Dictionary<string, int>();

            return Ok(new { PostId = parsedPostId, InteractionSummary = interactionSummary, TotalInteractions = response.Payload?.Count() ?? 0 });
        }
    }
}
