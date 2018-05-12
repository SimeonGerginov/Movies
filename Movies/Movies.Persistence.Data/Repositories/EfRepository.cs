﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Bytes2you.Validation;

using Movies.Core.Contracts;
using Movies.Core.Entities;

namespace Movies.Persistence.Data.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbSet<T> dbSet;
        private readonly MsSqlDbContext dbContext;

        public EfRepository(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, "Context").IsNull().Throw();

            this.dbContext = context;
            this.dbSet = this.dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            var entry = this.dbContext.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public IEnumerable<T> GetAll()
        {
            return this.dbSet.AsEnumerable();
        }

        public IEnumerable<T> GetAllFiltered(Expression<Func<T, bool>> filterExpression)
        {
            return this.dbSet
                .Where(filterExpression)
                .AsEnumerable();
        }

        public T GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public void Update(T entity)
        {
            var entry = this.dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
