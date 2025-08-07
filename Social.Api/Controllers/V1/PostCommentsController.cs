using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.Posts.Requests;
using Social.Api.Contracts.Posts.Responses;
using Social.Application.PostComments.Commands;
using Social.Application.PostComments.Queries;

namespace Social.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/posts/{postId}/comments")]
    [ApiController]
    public class PostCommentsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostCommentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostComments(string postId)
        {
            if (!Guid.TryParse(postId, out var parsedPostId))
                return BadRequest("Invalid post ID format");

            var query = new GetPostCommentsQuery { PostId = parsedPostId };
            var response = await _mediator.Send(query);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var comments = _mapper.Map<List<PostCommentResponse>>(response.Payload);
            return Ok(comments);
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetPostCommentById(string postId, string commentId)
        {
            if (!Guid.TryParse(commentId, out var parsedCommentId))
                return BadRequest("Invalid comment ID format");

            var query = new GetPostCommentByIdQuery { PostCommentId = parsedCommentId };
            var response = await _mediator.Send(query);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var comment = _mapper.Map<PostCommentResponse>(response.Payload);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPostComment(string postId, [FromBody] PostCommentCreateRequest request)
        {
            if (!Guid.TryParse(postId, out var parsedPostId))
                return BadRequest("Invalid post ID format");

            var command = _mapper.Map<AddPostCommentCommand>(request);
            command.PostId = parsedPostId;
            var response = await _mediator.Send(command);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var commentResponse = _mapper.Map<PostCommentResponse>(response.Payload);
            return CreatedAtAction(nameof(GetPostCommentById), 
                new { postId = postId, commentId = response.Payload?.PostCommentId }, 
                commentResponse);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdatePostComment(string postId, string commentId, [FromBody] PostCommentUpdateRequest request)
        {
            if (!Guid.TryParse(commentId, out var parsedCommentId))
                return BadRequest("Invalid comment ID format");

            var command = _mapper.Map<UpdatePostCommentCommand>(request);
            command.PostCommentId = parsedCommentId;
            var response = await _mediator.Send(command);
            
            return !response.IsSuccess ? HandleErrorResponse(response.Errors) : NoContent();
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeletePostComment(string postId, string commentId)
        {
            if (!Guid.TryParse(commentId, out var parsedCommentId))
                return BadRequest("Invalid comment ID format");

            var command = new DeletePostCommentCommand { PostCommentId = parsedCommentId };
            var response = await _mediator.Send(command);
            
            return !response.IsSuccess ? HandleErrorResponse(response.Errors) : NoContent();
        }
    }
}
