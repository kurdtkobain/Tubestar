using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival8 : Rival
    {
        public Rival8()
            : base()
        {
            Name = RivalStrings.Rival8Name.Translate();
            ShootingSkill = 45;
            PostProductionSkill = 35;
            InitialSubscribers = 3000;
            VideoKeyword1 = RivalStrings.Rival8Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival8Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival8Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival8Keyword4.Translate();
        }
    }
}