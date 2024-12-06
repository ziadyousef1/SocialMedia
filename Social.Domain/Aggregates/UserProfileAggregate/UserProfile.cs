using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Aggregates.UserProfileAggregate
{
    public class UserProfile
    {
        private UserProfile()
        {
            
        }
        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }

        public BasicInfo BasicInfo { get; private set; }

        public DateTime DateCreated { get; private set; }
        public DateTime LastDateModified { get; private set; }
        public static UserProfile Create(string identityId, BasicInfo basicInfo)
        {
            return new UserProfile
            {
              
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastDateModified = DateTime.UtcNow
            };
        }
        public void UpdateBasicInfo(BasicInfo basicInfo)
        {
            BasicInfo = basicInfo;
            LastDateModified = DateTime.UtcNow;
        }
    }
}
