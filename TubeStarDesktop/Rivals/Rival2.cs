using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival2 : Rival
    {
        public Rival2()
            : base()
        {
            Name = RivalStrings.Rival2Name.Translate();
            ShootingSkill = 30;
            PostProductionSkill = 20;
            InitialSubscribers = 70;
            VideoKeyword1 = RivalStrings.Rival2Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival2Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival2Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival2Keyword4.Translate();
        }
    }
}