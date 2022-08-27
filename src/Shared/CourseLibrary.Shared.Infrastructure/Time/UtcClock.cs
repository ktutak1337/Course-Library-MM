using CourseLibrary.Shared.Abstractions.Time;

namespace CourseLibrary.Shared.Infrastructure.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}
