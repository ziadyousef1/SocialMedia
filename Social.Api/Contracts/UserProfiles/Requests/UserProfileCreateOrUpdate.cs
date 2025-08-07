﻿namespace Social.Api.Contracts.UserProfile.Requests
{
    public class UserProfileCreateOrUpdate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; }
    }
}
