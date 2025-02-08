using System;
using System.ComponentModel;

namespace PlainCEETimer.Modules.Configuration
{
    public sealed class GeneralObject : ValidatableConfigObject
    {
        public ExamInfoObject[] ExamInfo
        {
            get => field ?? [];
            set
            {
                if (value == null)
                {
                    field = [];
                }
                else
                {
                    Validate(() =>
                    {
                        Array.Sort(value);
                    });

                    field = value;
                }
            }
        }

        public int ExamIndex
        {
            get => field;
            set
            {
                Validate(() =>
                {
                    if (value < 0 || value > ExamInfo.Length)
                    {
                        throw new Exception();
                    }
                });
            }
        }

        public bool MemClean { get; set; }

        [DefaultValue(true)]
        public bool TopMost { get; set; } = true;

        public bool UniTopMost { get; set; }
    }
}
