using System.Windows.Media;
using System.Xml.Serialization;

namespace TubeStar
{
    public enum SkillModifierType
    {
        Shooting,
        PostProduction,
        VideoAttribute,
        ViewQuality,
        None,
    }

    [XmlInclude(typeof(StudyAudienceAnalysisI))]
    [XmlInclude(typeof(StudyAudienceAnalysisII))]
    [XmlInclude(typeof(StudyPostProductionI))]
    [XmlInclude(typeof(StudyPostProductionII))]
    [XmlInclude(typeof(StudyPostProductionIII))]
    [XmlInclude(typeof(StudyProductionI))]
    [XmlInclude(typeof(StudyProductionII))]
    [XmlInclude(typeof(StudyProductionIII))]
    [XmlInclude(typeof(StudyQualityAnalysis))]
    public abstract class Study : Task
    {
        public abstract Study Prerequisite { get; }
        public abstract int Cost { get; }

        public abstract int SkillModifier { get; }
        public abstract SkillModifierType SkillModifierType { get; }

        public override TaskType TaskType
        {
            get { return TaskType.Study; }
        }

        public override Color Color
        {
            get { return Colors.DodgerBlue; }
        }
    }
}