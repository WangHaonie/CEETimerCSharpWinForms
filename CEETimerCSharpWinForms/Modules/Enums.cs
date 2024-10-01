using System;

namespace CEETimerCSharpWinForms.Modules
{
    public enum CountdownState
    {
        Normal,
        DaysOnly,
        DaysOnlyWithRounding,
        HoursOnly,
        MinutesOnly,
        SecondsOnly
    }

    public enum WorkingArea
    {
        Funny,
        SyncTime,
        SetPPTService,
        LastColor,
        SelectedColor,
        DefaultColor,
        ChangeFont,
        ShowLeftPast
    }

    public enum MessageLevel
    {
        Info,
        Warning,
        Error
    }

    public enum MessageBoxExButtons
    {
        OK,
        YesNo
    }

    public enum ExitReason
    {
        NormalExit = 0,
        AppUpdating = 1,
        UserShutdownOrRestart = 2,
        InvalidExeName = 3,
        AnotherInstanceIsRunning = 4
    }

    [Flags]
    public enum DialogExProp
    {
        BindButtons = 0B00100001,
        KeyPreview = 0B001000_10
    }
}
