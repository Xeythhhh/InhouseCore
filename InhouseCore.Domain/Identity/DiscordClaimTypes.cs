namespace InhouseCore.Domain.Identity;
public static class DiscordClaimTypes
{
    // https://discord.com/developers/docs/resources/user#user-object

    /// <summary>
    /// <para>Type: snowflake / ulong</para>
    /// <para>Description: The user's discordId.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string Id => "Discord:Id";

    /// <summary>
    /// <para>Type: string</para>
    /// <para>Description: The user's username, not unique across the platform.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string Username => "Discord:Username";

    /// <summary>
    /// <para>Type: string</para>
    /// <para>Description: The user's 4-digit discord-tag.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string Discriminator => "Discord:Discriminator";

    /// <summary>
    /// <para>Type: string?</para>
    /// <para>Description: The user's email.</para>
    /// <para>Scope: email</para>
    /// </summary>
    public static string Email => "Discord:Email";

    /// <summary>
    /// <para>Type: string?</para>
    /// <para>Description: The user's <see href="https://discord.com/developers/docs/reference#image-formatting">avatar hash</see>.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string AvatarUrl => "Discord:AvatarUrl";

    /// <summary>
    /// <para>Type: string?</para>
    /// <para>Description: The user's <see href="https://discord.com/developers/docs/reference#image-formatting">banner hash</see>.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string BannerUrl => "Discord:BannerUrl";

    /// <summary>
    /// <para>Type: boolean</para>
    /// <para>Description: Whether the user belongs to an OAuth2 application.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string IsBot => "Discord:IsBot";

    /// <summary>
    /// <para>Type: boolean</para>
    /// <para>Description: Whether the user has two factor enabled on their account.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string MfaEnabled => "Discord:MfaEnabled";

    /// <summary>
    /// <para>Type: integer?</para>
    /// <para>Description: The user's banner color encoded as an integer representation of hexadecimal color code.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string AccentColor => "Discord:AccentColor";

    /// <summary>
    /// <para>Type: string</para>
    /// <para>Description: The user's chosen <see href="https://discord.com/developers/docs/reference#locales">language option</see>.</para>
    /// <para>Scope: identify</para>
    /// </summary>
    public static string Locale => "Discord:Locale";

    /// <summary>
    /// <para>Type: boolean</para>
    /// <para>Description: Whether the email on this account has been verified.</para>
    /// <para>Scope: email</para>
    /// </summary>
    public static string Verified => "Discord:Verified";

}
