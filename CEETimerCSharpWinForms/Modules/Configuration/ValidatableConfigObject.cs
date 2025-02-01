using CEETimerCSharpWinForms.Forms;
using System;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    public class ValidatableConfigObject
    {
        protected void Validate(Action Method)
        {
            if (MainForm.ValidateNeeded)
            {
                Method();
            }
        }
    }
}
