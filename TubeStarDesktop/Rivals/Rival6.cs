using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival6 : Rival
    {
        public Rival6()
            : base()
        {
            Name = RivalStrings.Rival6Name.Translate();
            ShootingSkill = 40;
            PostProductionSkill = 30;
            InitialSubscribers = 800;
            VideoKeyword1 = RivalStrings.Rival6Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival6Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival6Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival6Keyword4.Translate();
        }
    }
}