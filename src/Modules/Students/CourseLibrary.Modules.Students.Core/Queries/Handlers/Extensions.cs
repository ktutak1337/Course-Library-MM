using CourseLibrary.Modules.Students.Core.DTOs;
using CourseLibrary.Modules.Students.Core.Entities;

namespace CourseLibrary.Modules.Students.Core.Queries.Handlers;

public static class Extensions
{
    public static StudentDto AsDto(this Student student)
        => student.Map<StudentDto>();

    public static StudentDetailsDto AsDetailsDto(this Student student)
    {
        var dto = student.Map<StudentDetailsDto>();
        dto.Notes = student?.Notes;

        return dto;
    }

    private static T Map<T>(this Student student) where T : StudentDto, new()
        => new()
        {
            Id = student.Id,
            Email = student.Email,
            FullName = student.FullName,
            Bio = student.Bio,
            AvatarUrl = student.AvatarUrl,
            IsActive = student.IsActive,
            Notes = student.Notes,
            State = student.GetState(),
            CreatedAt = student.CreatedAt
        };

    private static string GetState(this Student student) => student.IsActive ? "active" : "locked";
}