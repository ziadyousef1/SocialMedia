using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.Posts.Requests;
using Social.Api.Contracts.Posts.Responses;
using Social.Application.Posts.Commands;
using Social.Application.Posts.Queries;
using Social.Application.Services;

namespace Social.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class PostsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public PostsController(IMediator mediator, IMapper mapper, IFileUploadService fileUploadService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var query = new GetAllPostsQuery();
            var response = await _mediator.Send(query);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var posts = _mapper.Map<List<PostResponse>>(response.Payload);
            return Ok(posts);
        }

        [HttpGet(ApiRoutes.Posts.IdRoute)]
        public async Task<IActionResult> GetPostById(string id)
        {
            if (!Guid.TryParse(id, out var postId))
                return BadRequest("Invalid post ID format");

            var query = new GetPostByIdQuery { PostId = postId };
            var response = await _mediator.Send(query);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var post = _mapper.Map<PostResponse>(response.Payload);
            return Ok(post);
        }

        [HttpGet(ApiRoutes.Posts.UserRoute)]
        public async Task<IActionResult> GetPostsByUserId(string userId)
        {
            if (!Guid.TryParse(userId, out var userProfileId))
                return BadRequest("Invalid user ID format");

            var query = new GetPostsByUserIdQuery { UserProfileId = userProfileId };
            var response = await _mediator.Send(query);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var posts = _mapper.Map<List<PostResponse>>(response.Payload);
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostCreateRequest request)
        {
            var command = _mapper.Map<CreatePostCommand>(request);
            
            if (request.Photo != null)
            {
                try
                {
                    var (photoUrl, photoFileName) = await _fileUploadService.UploadFileAsync(request.Photo);
                    command.PhotoUrl = photoUrl;
                    command.PhotoFileName = photoFileName;
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error uploading photo: {ex.Message}");
                }
            }
            
            var response = await _mediator.Send(command);
            
            if (!response.IsSuccess)
                return HandleErrorResponse(response.Errors);
                
            var postResponse = _mapper.Map<PostResponse>(response.Payload);
            return CreatedAtAction(nameof(GetPostById), new { id = response.Payload?.PostId }, postResponse);
        }

        [HttpPut(ApiRoutes.Posts.IdRoute)]
        public async Task<IActionResult> UpdatePost(string id, [FromForm] PostUpdateRequest request)
        {
            if (!Guid.TryParse(id, out var postId))
                return BadRequest("Invalid post ID format");

            var command = _mapper.Map<UpdatePostCommand>(request);
            command.PostId = postId;
            
            if (request.Photo != null)
            {
                try
                {
                    var (photoUrl, photoFileName) = await _fileUploadService.UploadFileAsync(request.Photo);
                    command.PhotoUrl = photoUrl;
                    command.PhotoFileName = photoFileName;
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error uploading photo: {ex.Message}");
                }
            }
            
            var response = await _mediator.Send(command);
            
            return !response.IsSuccess ? HandleErrorResponse(response.Errors) : NoContent();
        }

        [HttpDelete(ApiRoutes.Posts.IdRoute)]
        public async Task<IActionResult> DeletePost(string id)
        {
            if (!Guid.TryParse(id, out var postId))
                return BadRequest("Invalid post ID format");

            var command = new DeletePostCommand { PostId = postId };
            var response = await _mediator.Send(command);
            
            return !response.IsSuccess ? HandleErrorResponse(response.Errors) : NoContent();
        }
    }
}
