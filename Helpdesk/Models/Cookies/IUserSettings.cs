namespace Helpdesk.Models.Cookies;

public interface IUserSettings
{
    public Guid UserId { get; set; }
    public Theme Theme { get; set; }
    public bool NotificationsEnabled { get; set; }

    public void SwitchTheme();
    public void SwtichNotificationsEnabled();
}
