using System;
using System.Xml.Serialization;

namespace TubeStar
{
    [Serializable]
    public enum AdvertisingStrategy
    {
        [XmlEnum("1")]
        [AdvertistingIncome(0.02)]
        Low = 1,

        [XmlEnum("2")]
        [AdvertistingIncome(0.04)]
        Normal = 2,

        [XmlEnum("3")]
        [AdvertistingIncome(0.06)]
        High = 3,

        [XmlEnum("4")]
        [AdvertistingIncome(0.08)]
        Aggressive = 4,
    }

    public static class AdvertisingStrategyHelpers
    {
        public static string GetString(this AdvertisingStrategy category)
        {
            switch (category)
            {
                case AdvertisingStrategy.Low: return EnglishStrings.Low.Translate();
                case AdvertisingStrategy.Normal: return EnglishStrings.Normal.Translate();
                case AdvertisingStrategy.High: return EnglishStrings.High.Translate();
                case AdvertisingStrategy.Aggressive: return EnglishStrings.Aggressive.Translate();
            }
            return String.Empty;
        }
    }
}