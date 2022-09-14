using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

public class InvalidUrlAddresssException : CourseLibraryException
{
    public string Url { get; }
    
    public InvalidUrlAddresssException(string url) 
        : base($"Invalid URL address: '{url}'")
    {
        Url = url;
    }
}
