namespace Social.Api
{
    public class ApiRoutes
    {
      public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public static class UserProfiles
        {
            public const string IdRoute =   "{id}";
        }
        public static class Posts
        {
            public const string IdRoute = "{id}";
            public const string UserRoute = "user/{userId}";
            
            
        }   
         public static class PostComments
        {
            public const string BaseRoute = "api/v{version:apiVersion}/posts/{postId}/comments";
            public const string IdRoute = "{commentId}";
        }

        public static class PostInteractions
        {
            public const string BaseRoute = "api/v{version:apiVersion}/posts/{postId}/interactions";
            public const string SummaryRoute = "summary";
        }
        public static class Identity
        {
            public const string Login           = "login";
            public const string Registration    = "registration";
            public const string IdentityById    = "{identityUserId}";
        }
    }
}
