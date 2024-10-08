﻿using CourseLibrary.Shared.Abstractions.Queries;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CourseLibrary.Shared.Infrastructure.Mongo;

public interface IMongoRepository<TEntity, in TIdentifiable> where TEntity : IIdentifiable<TIdentifiable>
{
	IMongoCollection<TEntity> Collection { get; }
	Task<TEntity> GetAsync(TIdentifiable id);
	Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
	Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

	Task<Paged<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
		TQuery query) where TQuery : IPagedQuery;

	Task AddAsync(TEntity entity);
	Task UpdateAsync(TEntity entity);
	Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
	Task DeleteAsync(TIdentifiable id);
	Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
	Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}
