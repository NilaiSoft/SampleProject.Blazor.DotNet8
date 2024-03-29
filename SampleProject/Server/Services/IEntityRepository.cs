﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using SampleProject.Core;
using System.Linq.Expressions;

namespace SampleProjects.Server.Services
{
    public interface IEntityRepository<TEntity, TVModel> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Table { get; }
        Task<EntityEntry<TEntity>> AddAsync(TEntity item);
        Task AddRangeAsync(IList<TEntity> items);
        Task<int> AddAndSaveChangesAsync(TEntity entity);
        Task<int> AddRangeAndSaveChangesAsync(IList<TEntity> entitys);
        Task<int> SaveChangesAsync();

        #region EditUseZEntity
        //Task<int> EditAsync(Expression<Func<TEntity, bool>> predicate,
        //    Expression<Func<TEntity, TEntity>> expression);
        #endregion

        Task<int> EditAsync(TEntity entity);
        Task<int> EditAsync(Expression<Func<TEntity, TEntity>> predicate
        , Expression<Func<TEntity, TEntity>> entity);
        Task<TEntity?> GetAsync
            (Expression<Func<TEntity, bool>> _pridicate, Expression<Func<TEntity, TEntity>> selectItem);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> _pridicate);
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> _pridicate);
        Task<IList<TEntity>> GetAllAsync
        (Expression<Func<TEntity, bool>> _pridicate,
            Expression<Func<TEntity, TEntity>> selectList);
        Task<IPagedList<TEntity>> GetAllAsync
            (Expression<Func<TEntity, bool>> _pridicate
            , Expression<Func<TEntity, TEntity>> _selectList, int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IPagedList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> _pridicate, int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IPagedList<TEntity>> GetAllAsync(int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, TEntity>> selectList);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> _pridicate);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> _pridicate
            , Expression<Func<TEntity, TEntity>> expression);
        Task<TEntity?> FindAsync(int Id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> AnyAsync();
    }
}
