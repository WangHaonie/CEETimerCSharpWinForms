using System;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    public sealed class ToolsObject : ValidatableConfigObject
    {
        public int NtpServer
        {
            get => field;
            set
            {
                Validate(() =>
                {
                    if (value is < 0 or > 3)
                    {
                        throw new Exception();
                    }
                });

                field = value;
            }
        }

        public bool TrayIcon { get; set; }

        public bool TrayText { get; set; }
    }
}
