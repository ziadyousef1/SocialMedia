using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.UserProfiles.Queries;
using Social.DAL;
using Social.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social.Application.Models;
using Social.Application.Enums;

namespace Social.Application.UserProfiles.QueryHandlers
{
    public class GellAllUserProfilesQueryHandler : IRequestHandler<GellAllUserProfilesQuery, OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly DataContext _dataContext;

        public GellAllUserProfilesQueryHandler(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GellAllUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<IEnumerable<UserProfile>>();
            try
            {
                var profiles = await _dataContext.UserProfiles.ToListAsync();
                operationResult.Payload = profiles;
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
