namespace Helpdesk.Models.Cookies;

public sealed class UserSettings : IUserSettings
{
    public Guid UserId { get; set; }
    public Theme Theme { get; set; }
    public bool NotificationsEnabled { get; set; }

    public UserSettings()
    {
        UserId = Guid.NewGuid();
        Theme = Theme.Light;
        NotificationsEnabled = true;
    }

    public void SwitchTheme()
    {
        if (Theme == Theme.Light)
            Theme = Theme.Dark;
        else if (Theme == Theme.Dark)
            Theme = Theme.Light;
    }

    public void SwtichNotificationsEnabled()
    {
        NotificationsEnabled = !NotificationsEnabled;
    }

    public static string Serialize(IUserSettings settings)
    {
        if (settings is UserSettings)
            return JsonSerializer.Serialize(settings as UserSettings);
        else
            throw new Exception("Trying to use UserSettings serialization static method on different implementation of IUserSettings, possible errors.");
    }

    public static IUserSettings? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<UserSettings>(json);
    }
}
