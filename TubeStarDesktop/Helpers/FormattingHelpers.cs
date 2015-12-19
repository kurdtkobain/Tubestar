using System;
using System.Globalization;

namespace TubeStar
{
    public static class FormattingHelpers
    {
        private static readonly CultureInfo CurrentCulture = new CultureInfo("en-US");

        public static string ToCurrencyString(this int instance)
        {
            return ((double)instance).ToCurrencyString();
        }

        public static string ToCurrencyString(this double instance)
        {
            instance = Math.Round(instance, 2);
            return instance.ToString("C", CurrentCulture);
        }

        public static string ToNumberString(this int instance)
        {
            return instance.ToString("N0", CurrentCulture);
        }
    }
}