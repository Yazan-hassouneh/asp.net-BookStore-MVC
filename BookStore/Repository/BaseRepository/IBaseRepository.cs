﻿using System.Linq.Expressions;

namespace BookStore.Repository.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindAsync(Expression<Func<T, bool>> matcher);
        Task<T?> FindAsync(Expression<Func<T, bool>> matcher, string[] Includes);
        Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> matcher, string[] Includes);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
