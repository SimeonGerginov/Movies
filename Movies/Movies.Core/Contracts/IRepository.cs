using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Movies.Core.Contracts
{
    public interface IRepository<T> where T : class, IAuditable, IDeletable
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllFiltered(Expression<Func<T, bool>> filterExpression);

        IEnumerable<T> GetAllAndIncludeEntity(string entityToInclude);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
