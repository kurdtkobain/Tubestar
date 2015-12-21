using System.Collections.Generic;

namespace TubeStar
{
    public class SaveObj
    {
        public Player Player { get; set; }
        public List<SerializableShareData> ShareViews { get; set; }
        public List<SerializableShareData> BoughtViews { get; set; }
        public Studies Studies { get; set; }
        public StoreItems StoreItems { get; set; }
        public List<Rival> Rivals { get; set; }
    }
}