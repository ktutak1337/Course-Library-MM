using CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.ValueObjects;

public sealed record Url
{
    public string Value { get; }

    public Url(string value)
    {
        value = value.ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidUrlAddresssException(value);
        }
        
        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
        {
            throw new InvalidUrlAddresssException(value);
        }

        Value = value;
    }
    
    public static implicit operator string(Url url) => url.Value;
    public static implicit operator Url(string url) => new Url(url);
}
