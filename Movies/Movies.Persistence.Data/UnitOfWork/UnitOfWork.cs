﻿using Bytes2you.Validation;
using Movies.Core.Contracts;

namespace Movies.Persistence.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MsSqlDbContext dbContext;

        public UnitOfWork(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, "Context").IsNull().Throw();

            this.dbContext = context;
        }

        public void Complete()
        {
            if (!this.dbContext.ChangeTracker.HasChanges())
            {
                return;
            }

            this.dbContext.SaveChanges();
        }
    }
}
