﻿namespace CourseLibrary.Shared.Infrastructure.Mongo;

public interface IIdentifiable<out T>
{
    T Id { get; }
}
