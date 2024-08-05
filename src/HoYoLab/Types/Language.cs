namespace HoYoLab.Types;

public enum Language : byte
{
    English,
    TraditionalChinese,
    Japanese,
    SimplifiedChinese
}

public static class LanguageExtensions
{
    public static string ToCode(this Language lang) => lang switch
    {
        Language.English => "en",
        Language.TraditionalChinese => "zh-tw",
        Language.Japanese => "ja",
        Language.SimplifiedChinese => "zh-cn",
        _ => throw new IndexOutOfRangeException(nameof(lang))
    };
}
