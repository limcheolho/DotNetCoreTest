namespace TestWebApi;

public static class StringExtensions
{
    public static string TruncateString(this string value, int length = 500) =>
        value.Length > length ? value.Substring(0, length) : value;
}