namespace Social.Api
{
    public class ApiRoutes
    {
      public const string BaseRoute = "api/[controller]";

        public static class UserProfiles
        {
            public const string IdRoute =   "{id}";
        }
        public static class Posts
        {
            public const string GetById = "{id}";
        }   
    }
}
