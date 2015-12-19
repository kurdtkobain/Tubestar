using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival5 : Rival
    {
        public Rival5()
            : base()
        {
            Name = RivalStrings.Rival5Name.Translate();
            ShootingSkill = 40;
            PostProductionSkill = 25;
            InitialSubscribers = 500;
            VideoKeyword1 = RivalStrings.Rival5Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival5Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival5Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival5Keyword4.Translate();
        }
    }
}