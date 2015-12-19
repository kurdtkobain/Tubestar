using System;
using System.Windows.Media;

namespace TubeStar
{
    public class Job : Task
    {
        public override TaskType TaskType
        {
            get { return TaskType.Job; }
        }

        public override string Name
        {
            get { return EnglishStrings.Job.Translate(); }
        }

        public override Color Color
        {
            get { return Colors.Crimson; }
        }

        public override int HoursToComplete
        {
            get { return 8; }
        }
    }
}