namespace Helpdesk.Models.Settings;

/// <summary>
/// Implementation of IApplicationSettings, contains all required data to run application. Primarily uses 'appoptions.json' file.
/// </summary>
public class ApplicationOptions
{
    public NavigationOptions NavigationOptions { get; set; } = new();
}
