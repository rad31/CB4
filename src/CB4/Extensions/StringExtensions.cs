namespace CB4.Extensions;

public static class StringExtensions
{
    public static string ToCamelCase(this string name)
    {
        var firstLetter = name.Substring(0, 1).ToLower();
        var remaining = name.Substring(1);
        return $"{firstLetter}{remaining}";
    }
}
