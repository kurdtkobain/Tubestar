using System;
using System.Windows.Media;

namespace TubeStar
{
    public class BowToRobotRulers : Task
    {
        public override TaskType TaskType
        {
            get { return TaskType.BowToRobotRulers; }
        }

        public override string Name
        {
            get { return EnglishStrings.MandatoryBowToRobotRulers.Translate(); }
        }

        public override Color Color
        {
            get { return Colors.Black; }
        }

        public override int HoursToComplete
        {
            get { return 3; }
        }
    }
}