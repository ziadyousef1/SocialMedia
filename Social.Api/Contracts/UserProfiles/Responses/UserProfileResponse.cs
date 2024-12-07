using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Api.Contracts.UserProfile.Responses
{
    public record UserProfileResponse
    {
        public Guid UserProfileId { get;  set; }

        public BasicInformation BasicInfo { get;  set; }

        public DateTime DateCreated { get;  set; }
        public DateTime LastDateModified { get;  set; }
    }
}
