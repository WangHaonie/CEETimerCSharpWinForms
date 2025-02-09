using Newtonsoft.Json;
using PlainCEETimer.Modules.JsonConverters;
using System;

namespace PlainCEETimer.Modules.Configuration
{
    public sealed class ExamInfoObject : ValidatableConfigObject, IComparable<ExamInfoObject>
    {
        public string ExamName
        {
            get => field;
            set
            {
                Validate(() =>
                {
                    if (!value.Length.IsValid())
                    {
                        throw new Exception();
                    }
                });

                field = value.RemoveIllegalChars();
            }
        } = "";

        [JsonConverter(typeof(ExamTimeConverter))]
        public DateTime ExamStartTime { get; set; } = DateTime.Now;

        [JsonConverter(typeof(ExamTimeConverter))]
        public DateTime ExamEndTime { get; set; } = DateTime.Now;

        public override string ToString()
            => string.Format("{0} - {1}", ExamName, ExamStartTime.ToString(AppLauncher.DateTimeFormat));

        int IComparable<ExamInfoObject>.CompareTo(ExamInfoObject other)
        {
            if (other == null)
            {
                return 1;
            }

            return ExamStartTime.CompareTo(other.ExamStartTime);
        }
    }
}
