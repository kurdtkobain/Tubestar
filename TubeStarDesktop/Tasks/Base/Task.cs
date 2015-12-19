using System;
using System.Windows.Media;
using System.Xml.Serialization;

namespace TubeStar
{
    [XmlInclude(typeof(EditVideo))]
    [XmlInclude(typeof(Job))]
    [XmlInclude(typeof(BowToRobotRulers))]
    [XmlInclude(typeof(ShootVideo))]
    [XmlInclude(typeof(Study))]
    [XmlInclude(typeof(StudyAudienceAnalysisI))]
    [XmlInclude(typeof(StudyAudienceAnalysisII))]
    [XmlInclude(typeof(StudyPostProductionI))]
    [XmlInclude(typeof(StudyPostProductionII))]
    [XmlInclude(typeof(StudyPostProductionIII))]
    [XmlInclude(typeof(StudyProductionI))]
    [XmlInclude(typeof(StudyProductionII))]
    [XmlInclude(typeof(StudyProductionIII))]
    [XmlInclude(typeof(StudyQualityAnalysis))]
    public abstract class Task
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }
        public abstract int HoursToComplete { get; }
        public abstract TaskType TaskType { get; }

        public int ExtraHours { get; set; }
        public int HoursPutIn { get; set; }

        public bool IsCompleted
        {
            get { return HoursLeft < 1; }
        }

        public int HoursLeft
        {
            get { return HoursToComplete + ExtraHours - HoursPutIn; }
        }
    }
}