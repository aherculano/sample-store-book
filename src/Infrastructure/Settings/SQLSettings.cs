﻿namespace Infrastructure.Settings;

public class SQLSettings
{
    public const string SettingsSection = "SqlSettings";
    
    public string ConnectionString { get; set; }
}