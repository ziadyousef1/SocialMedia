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

namespace Social.Application.UserProfiles.QueryHandlers
{
    public class GellAllUserProfilesQueryHandler : IRequestHandler<GellAllUserProfilesQuery, IEnumerable<UserProfile>>
    {
        private readonly DataContext _dataContext;

        public GellAllUserProfilesQueryHandler(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<IEnumerable<UserProfile>> Handle(GellAllUserProfilesQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.UserProfiles.ToListAsync();

        }
    }
}
