using System;
using System.Xml.Serialization;

namespace TubeStar
{
    [Serializable]
    public enum VideoCategory
    {
        [XmlEnum("1")]
        UkuleleCover = 1,

        [XmlEnum("2")]
        AnimalVSAnimal = 2,

        [XmlEnum("3")]
        Technology = 3,

        [XmlEnum("4")]
        MusicVideo = 4,

        [XmlEnum("5")]
        TheWeirdSide = 5,

        [XmlEnum("6")]
        Sports = 6,

        [XmlEnum("7")]
        Comedy = 7,

        [XmlEnum("8")]
        Gaming = 8,

        [XmlEnum("9")]
        HowTo = 9,

        [XmlEnum("10")]
        Hauls = 10,

        [XmlEnum("11")]
        Cats = 11,

        [XmlEnum("12")]
        ConspiraryTheories = 12,

        [XmlEnum("13")]
        Vlog = 13,

        [XmlEnum("14")]
        Accidents = 14,

        [XmlEnum("15")]
        NonProfit = 15,

        [XmlEnum("16")]
        Anime = 16,

        [XmlEnum("17")]
        Movies = 17,

        [XmlEnum("18")]
        Creepypasta = 18,
    }

    public static class VideoCategoryHelpers
    {
        public static string GetString(this VideoCategory category)
        {
            switch (category)
            {
                case VideoCategory.Accidents: return EnglishStrings.Fails.Translate();
                case VideoCategory.AnimalVSAnimal: return EnglishStrings.AnimalFighting.Translate();
                case VideoCategory.Anime: return EnglishStrings.Anime.Translate();
                case VideoCategory.Cats: return EnglishStrings.Cats.Translate();
                case VideoCategory.Comedy: return EnglishStrings.Comedy.Translate();
                case VideoCategory.ConspiraryTheories: return EnglishStrings.ConspiraryTheory.Translate();
                case VideoCategory.Creepypasta: return EnglishStrings.CreepyPasta.Translate();
                case VideoCategory.Gaming: return EnglishStrings.Gaming.Translate();
                case VideoCategory.Hauls: return EnglishStrings.ShoppingHaul.Translate();
                case VideoCategory.HowTo: return EnglishStrings.HowTo.Translate();
                case VideoCategory.Movies: return EnglishStrings.Movies.Translate();
                case VideoCategory.MusicVideo: return EnglishStrings.MusicVideo.Translate();
                case VideoCategory.NonProfit: return EnglishStrings.NonProfit.Translate();
                case VideoCategory.Sports: return EnglishStrings.Sports.Translate();
                case VideoCategory.TheWeirdSide: return EnglishStrings.TheWeirdSide.Translate();
                case VideoCategory.Technology: return EnglishStrings.Technology.Translate();
                case VideoCategory.UkuleleCover: return EnglishStrings.UkuleleCover.Translate();
                case VideoCategory.Vlog: return EnglishStrings.Vlog.Translate();
            }
            return String.Empty;
        }
    }
}