using System;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    public sealed class ConfigLoadedEventArgs(ConfigObject Config) : EventArgs
    {
        public ConfigObject ConfigObject { get; set; } = Config;
    }
}
