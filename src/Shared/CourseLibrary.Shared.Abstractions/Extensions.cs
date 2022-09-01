using System.Text.RegularExpressions;

namespace CourseLibrary.Shared.Abstractions;

public static class Extensions
{
    public static string RemoveWhitespace(this string value)
        => string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, @"\s+", string.Empty);

    public static long ToUnixTimeMilliseconds(this DateTime dateTime)
            => new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();

    public static async Task<T> NotNull<T>(this Task<T> task, Func<Exception> exception = null)
    {
        if (task is null)
        {
            throw new InvalidOperationException("Task cannot be null.");
        }

        var result = await task;
        if (result is not null)
        {
            return result;
        }

        if (exception is not null)
        {
            throw exception();

        }

        throw new InvalidOperationException("Returned result is null.");
    }
}
