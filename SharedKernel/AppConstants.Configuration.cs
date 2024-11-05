namespace SharedKernel;

public static partial class AppConstants
{
    public static class Configuration
    {
        public const string ChampionRoles = "Domain:ChampionRoles";
        public const string AugmentTargets = "Domain:AugmentTargets";

        public const string EfCoreIdGenId = "IdGen:EfCore";

        public static class Discord
        {
            public const string ConfigurationSection = "Discord";
            public const string ClientId = "Discord:AppId";
            public const string ClientSecret = "Discord:AppSecret";
            public const string BotToken = "Discord:BotToken";
        }

        public static class ConnectionStrings
        {
            public const string ApplicationSqlServer = "ConnectionStrings:ApplicationSqlServer";
        }
    }
}
