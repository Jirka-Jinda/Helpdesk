namespace Helpdesk.Models.Cookies;

public interface IUserSettings
{
    public Guid UserId { get; set; }
    public Theme Theme { get; set; }

    public void SwitchTheme();
    //public static abstract string Serialize(IUserSettings settings);
    //public static abstract IUserSettings Deserialize(string json);
}
