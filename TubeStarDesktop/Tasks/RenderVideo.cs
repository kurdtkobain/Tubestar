using System;
using System.Windows.Media;

namespace TubeStar
{
    public class RenderVideo : Task
    {
        public const int MinimumRenderTime = 2;

        public Video Video { get; set; }

        public RenderVideo()
        {}
        
        public RenderVideo(Video video)
        {
            Video = video;
        }
        
        public override TaskType TaskType
        {
            get { return TaskType.RenderVideo; }
        }

        public override string Name
        {
            get { return String.Format(EnglishStrings.RenderVideoTask.Translate(), Video.Name); }
        }

        public override Color Color
        {
            get { return Colors.Purple; }
        }

        public override int HoursToComplete
        {
            get { return MinimumRenderTime; }
        }
    }
}
