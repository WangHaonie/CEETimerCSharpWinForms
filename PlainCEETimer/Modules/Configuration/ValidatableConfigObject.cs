using PlainCEETimer.Forms;
using System;

namespace PlainCEETimer.Modules.Configuration
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
