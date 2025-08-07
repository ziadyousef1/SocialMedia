using AutoMapper;
using Social.Api.Contracts.Posts.Requests;
using Social.Api.Contracts.Posts.Responses;
using Social.Application.PostComments.Commands;
using Social.Application.PostInteractions.Commands;
using Social.Application.Posts.Commands;
using Social.Domain.Aggregates.PostAggregate;
using PostEntity = Social.Domain.Aggregates.PostAggregate.Post;

namespace Social.Api.MappingProfiles
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<PostCreateRequest, CreatePostCommand>();
            CreateMap<PostUpdateRequest, UpdatePostCommand>();
            CreateMap<PostEntity, PostResponse>()
                .ForMember(dest => dest.UserProfileId, opt => opt.MapFrom(src => src.UserProfileID))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.InteractionCount, opt => opt.MapFrom(src => src.Interactions.Count));
            
            CreateMap<PostInteractionRequest, AddPostInteractionCommand>();
            CreateMap<PostInteraction, PostInteractionResponse>();
            
            CreateMap<PostCommentCreateRequest, AddPostCommentCommand>();
            CreateMap<PostCommentUpdateRequest, UpdatePostCommentCommand>();
            CreateMap<PostComment, PostCommentResponse>();
        }
    }
}
