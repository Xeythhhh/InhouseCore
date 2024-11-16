namespace SharedKernel;

public static partial class AppConstants
{
    public static class Discord
    {
        public const string OAuthCallback = "/oauth/discord/callback";

        public static class Scopes
        {
            public const string Email = "email";
            public const string GroupDmJoin = "gdm.join";
            public const string Guilds = "guilds";
        }

        public static class Claims
        {
            public const string Id = "discord:id";
            public const string Username = "discord:username";
            public const string Verified = "discord:verified";
            public const string Avatar = "discord:avatar";

            public static class Keys
            {
                public const string Id = "id";
                public const string UserName = "username";
                public const string Verified = "verified";
                public const string Avatar = "avatar";
            }
        }
    }
}
