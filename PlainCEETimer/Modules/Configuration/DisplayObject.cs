﻿using System;
using System.Windows.Forms;

namespace PlainCEETimer.Modules.Configuration
{
    public sealed class DisplayObject : ValidatableConfigObject
    {
        public bool ShowXOnly { get; set; }

        public int X
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

        public bool Rounding { get; set; }

        public int EndIndex
        {
            get => field;
            set
            {
                Validate(() =>
                {
                    if (value is < 0 or > 2)
                    {
                        throw new Exception();
                    }
                });

                field = value;
            }
        }

        public bool CustomText { get; set; }

        public string[] CustomTexts { get; set; }
            = [Placeholders.PH_P1, Placeholders.PH_P2, Placeholders.PH_P3];

        public int ScreenIndex
        {
            get => field;
            set
            {
                Validate(() =>
                {
                    if (value < 0 || value > Screen.AllScreens.Length)
                    {
                        throw new Exception();
                    }
                });

                field = value;
            }
        }

        public CountdownPosition Position { get; set; }

        public bool Draggable { get; set; }

        public bool SeewoPptsvc { get; set; }
    }
}
