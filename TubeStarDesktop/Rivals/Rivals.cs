using System;
using System.Collections.Generic;

namespace TubeStar
{
    public class Rivals
    {
        private static Rivals _current;

        public static Rivals Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Rivals();
                    if (!String.IsNullOrEmpty(Settings.RivalsModPath))
                    {
                        try
                        {
                            var xml = System.IO.File.ReadAllText(Settings.RivalsModPath);
                            _current.PopulateFromList(SerializationHelpers.FromXml<List<Rival>>(xml));
                        }
                        catch
                        {
                            _current = new Rivals();
                        }
                    }
                }
                return _current;
            }
            set { _current = value; }
        }

        private List<Rival> _rivals;

        public List<Rival> All
        {
            get { return _rivals; }
        }

        public Rivals()
        {
            _rivals = new List<Rival>();
            _rivals.Add(new Rival1());
            _rivals.Add(new Rival2());
            _rivals.Add(new Rival3());
            _rivals.Add(new Rival4());
            _rivals.Add(new Rival5());
            _rivals.Add(new Rival6());
            _rivals.Add(new Rival7());
            _rivals.Add(new Rival8());
            _rivals.Add(new Rival9());
            _rivals.Add(new Rival10());
            _rivals.Add(new Rival11());
            _rivals.Add(new Rival12());
            _rivals.Add(new Rival13());
            _rivals.Add(new Rival14());
            _rivals.Add(new Rival15());
        }

        public void PopulateFromList(List<Rival> rivals)
        {
            _rivals = rivals;
        }
    }
}