using System;
using System.Collections.Generic;
using System.Linq;

namespace TubeStar
{
    public class Studies
    {
        private static Studies _current;
        public static Studies Current
        {
            get
            {
                if (_current == null)
                    _current = new Studies();
                return _current;
            }
            set { _current = value; }
        }

        public StudyProductionI ProductionI { get; set; }
        public StudyProductionII ProductionII { get; set; }
        public StudyProductionIII ProductionIII { get; set; }

        public StudyPostProductionI PostProductionI { get; set; }
        public StudyPostProductionII PostProductionII { get; set; }
        public StudyPostProductionIII PostProductionIII { get; set; }

        public StudyAudienceAnalysisI AudienceAnalysisI { get; set; }
        public StudyAudienceAnalysisII AudienceAnalysisII { get; set; }

        public StudyQualityAnalysis QualityAnalysis { get; set; }

        public List<Study> All
        {
            get
            {
                return new List<Study>()
                {
                    ProductionI,
                    ProductionII,
                    ProductionIII,

                    PostProductionI,
                    PostProductionII,
                    PostProductionIII,

                    AudienceAnalysisI,
                    AudienceAnalysisII,

                    QualityAnalysis,
                };
            }
        }

        public Studies()
        {
            ProductionI = new StudyProductionI();
            ProductionII = new StudyProductionII();
            ProductionIII = new StudyProductionIII();

            PostProductionI = new StudyPostProductionI();
            PostProductionII = new StudyPostProductionII();
            PostProductionIII = new StudyPostProductionIII();

            AudienceAnalysisI = new StudyAudienceAnalysisI();
            AudienceAnalysisII = new StudyAudienceAnalysisII();

            QualityAnalysis = new StudyQualityAnalysis();
        }
    }
}