using System;

namespace ccU3DEngine
{
    public enum Locale
    {
        None,
        zhCN,
        zhTW,
        enUS,
        enGB,
        frFR,
        deDE,
        koKR,
        esES,
        esMX,
        ruRU,
        itIT,
        ptBR,
        ptPT,
        plPL
    }

    public static class LocaleExtend
    {
        public static string ToLocalizedString(this Locale locale)
        {
            switch (locale)
            {
                case Locale.zhCN: return "华语 (大陆)";
                case Locale.zhTW: return "華語 (台灣)";
                case Locale.enUS: return "English (United States)";
                case Locale.enGB: return "English (United Kingdom)";
                case Locale.frFR: return "français";
                case Locale.deDE: return "Deutsche";
                case Locale.koKR: return "한국어";
                default:
                    return $"!({locale})!";
            }
        }
    }
}