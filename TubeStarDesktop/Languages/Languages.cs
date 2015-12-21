using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace TubeStar
{
    public static class Languages
    {
        public class CustomText
        {
            public EnglishStrings Id { get; set; }
            public string Text { get; set; }
        }

        private static Dictionary<EnglishStrings, string> _customLanguage;
        private static Dictionary<CommentStrings, string> _comments;
        private static Dictionary<RivalStrings, string> _rivalStrings;

        public static void SetLanguage(string language)
        {
            if (_customLanguage == null)
                _customLanguage = new Dictionary<EnglishStrings, string>();

            try
            {
                var streamResourceInfo = Application.GetResourceStream(new Uri(String.Format("pack://application:,,,/Languages/{0}.xml", language)));
                using (var stream = streamResourceInfo.Stream)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var xml = reader.ReadToEnd();
                        foreach (var customText in SerializationHelpers.FromXml<List<CustomText>>(xml))
                        {
                            _customLanguage[customText.Id] = customText.Text;
                        }
                        Properties.Settings.Default.CustomModPath = null;
                        Properties.Settings.Default.Language = language;
                    }
                }
            }
            catch (Exception ex)
            {
                _customLanguage = null;
                Properties.Settings.Default.Language = null;
            }
        }

        public static void ReadLanguage(string fileName)
        {
            if (_customLanguage == null)
                _customLanguage = new Dictionary<EnglishStrings, string>();

            try
            {
                var xml = File.ReadAllText(fileName);
                foreach (var customText in SerializationHelpers.FromXml<List<CustomText>>(xml))
                {
                    _customLanguage[customText.Id] = customText.Text;
                }
                Properties.Settings.Default.CustomModPath = fileName;
            }
            catch
            {
                _customLanguage = null;
                Properties.Settings.Default.CustomModPath = null;
            }
        }

        public static void Reset()
        {
            _customLanguage = null;
            _comments = null;
            _rivalStrings = null;
        }

        public static string Translate(this EnglishStrings english)
        {
            if (_customLanguage == null)
                _customLanguage = new Dictionary<EnglishStrings, string>();

            if (!_customLanguage.ContainsKey(english))
                _customLanguage[english] = EnumHelpers.GetAttribute<DescriptionAttribute>(english).Description;
            return _customLanguage[english];
        }

        public static string Translate(this CommentStrings comment)
        {
            if (_comments == null)
                _comments = new Dictionary<CommentStrings, string>();

            if (!_comments.ContainsKey(comment))
                _comments[comment] = EnumHelpers.GetAttribute<DescriptionAttribute>(comment).Description;
            return _comments[comment];
        }

        public static string Translate(this RivalStrings rivalString)
        {
            if (_rivalStrings == null)
                _rivalStrings = new Dictionary<RivalStrings, string>();

            if (!_rivalStrings.ContainsKey(rivalString))
                _rivalStrings[rivalString] = EnumHelpers.GetAttribute<DescriptionAttribute>(rivalString).Description;
            return _rivalStrings[rivalString];
        }

        public static string SerializeEnglish()
        {
            List<CustomText> language = new List<CustomText>();
            foreach (EnglishStrings english in Enum.GetValues(typeof(EnglishStrings)))
            {
                language.Add(new CustomText()
                {
                    Id = english,
                    Text = EnumHelpers.GetAttribute<DescriptionAttribute>(english).Description,
                });
            }
            return SerializationHelpers.ToXml(language);
        }

        public static string SerializeTranslation(Dictionary<EnglishStrings, string> translation)
        {
            List<CustomText> language = new List<CustomText>();
            foreach (EnglishStrings english in Enum.GetValues(typeof(EnglishStrings)))
            {
                language.Add(new CustomText()
                {
                    Id = english,
                    Text = translation.ContainsKey(english) && !String.IsNullOrEmpty(translation[english]) ? translation[english] : EnumHelpers.GetAttribute<DescriptionAttribute>(english).Description,
                });
            }
            return SerializationHelpers.ToXml(language);
        }
    }
}