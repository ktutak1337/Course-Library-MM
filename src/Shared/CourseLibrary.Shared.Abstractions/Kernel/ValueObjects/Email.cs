using System.Text.RegularExpressions;
using CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.ValueObjects;

public sealed record Email
{
    private static readonly Regex Regex = new(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.Compiled);
    
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailAddressException(value);
        }

        var parts = value.Split("@");
        var username = parts[0];
        var domain = parts[1];
        
        if (username.Length > 64 || domain.Length > 255)
        {
            throw new InvalidEmailAddressException(value);
        }

        value = value.ToLowerInvariant();
        
        if (!Regex.IsMatch(value))
        {
            throw new InvalidEmailAddressException(value);
        }

        Value = value;
    }
    
    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new Email(email);
}
