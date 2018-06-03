﻿using System.Linq;
using Bytes2you.Validation;

using Movies.Core.Models;
using Movies.Persistence.Data;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class UserService : IUserService
    {
        private readonly MsSqlDbContext context;

        public UserService(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, "Context").IsNull().Throw();

            this.context = context;
        }

        public User GetUser(string username)
        {
            return this.context
                .Users
                .FirstOrDefault(u => u.UserName == username);
        }
    }
}
