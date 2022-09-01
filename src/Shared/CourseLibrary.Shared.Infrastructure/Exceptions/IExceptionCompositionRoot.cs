using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}
