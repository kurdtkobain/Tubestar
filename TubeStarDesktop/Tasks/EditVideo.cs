using System;
using System.Windows.Media;

namespace TubeStar
{
    public class EditVideo : Task
    {
        public const int MinimumEditTime = 4;

        public Video Video { get; set; }
        public int Episodes { get; set; }

        public EditVideo()
        { }

        public EditVideo(Video video)
        {
            Video = video;
        }

        public override TaskType TaskType
        {
            get { return TaskType.EditVideo; }
        }

        public override string Name
        {
            get { return String.Format(EnglishStrings.EditVideoTask.Translate(), Video.Name); }
        }

        public override Color Color
        {
            get { return Colors.BlueViolet; }
        }

        public override int HoursToComplete
        {
            get { return MinimumEditTime; }
        }
    }
}