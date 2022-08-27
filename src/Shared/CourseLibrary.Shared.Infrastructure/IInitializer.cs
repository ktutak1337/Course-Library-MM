namespace CourseLibrary.Shared.Infrastructure;

public interface IInitializer
{
    ValueTask InitAsync();
}
