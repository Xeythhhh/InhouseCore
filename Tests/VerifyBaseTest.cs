namespace Tests;
public abstract class VerifyBaseTest
{
    internal VerifySettings Settings { get; set; }

    protected VerifyBaseTest()
    {
        Settings = new VerifySettings();
        Settings.UseDirectory(".Snapshots");
    }
}
