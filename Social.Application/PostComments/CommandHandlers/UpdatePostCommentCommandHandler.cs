using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.PostComments.Commands;
using Social.Application.Models;
using Social.Application.Enums;
using Social.DAL;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.PostComments.CommandHandlers
{
    public class UpdatePostCommentCommandHandler : IRequestHandler<UpdatePostCommentCommand, OperationResult<PostComment>>
    {
        private readonly DataContext _context;

        public UpdatePostCommentCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PostComment>> Handle(UpdatePostCommentCommand request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<PostComment>();
            try
            {
                var comment = await _context.PostComments
                    .FirstOrDefaultAsync(c => c.PostCommentId == request.PostCommentId, cancellationToken);

                if (comment == null)
                {
                    operationResult.IsSuccess = false;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"Comment with id {request.PostCommentId} not found."
                    };
                    operationResult.Errors.Add(error);
                    return operationResult;
                }

                comment.UpdateText(request.Text);
                _context.PostComments.Update(comment);
                await _context.SaveChangesAsync(cancellationToken);
                operationResult.Payload = comment;
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
}
