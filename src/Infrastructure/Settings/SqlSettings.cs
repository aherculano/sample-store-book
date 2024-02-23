namespace Infrastructure.Settings;

public class SqlSettings
{
    public const string SettingsSection = "SqlSettings";
    
    public string ConnectionString { get; set; }
}