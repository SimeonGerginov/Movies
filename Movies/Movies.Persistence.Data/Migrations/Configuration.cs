using System;
using System.Linq;
using System.Data.Entity.Migrations;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Movies.Common;
using Movies.Core.Models;

namespace Movies.Persistence.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        private const string AdminUsername = "Simeon";
        private const string AdminFirstName = "Simeon";
        private const string AdminLastName = "Gerginov";
        private const Gender AdminGender = Gender.Male;
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPassword = "123456";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            this.SeedUsers(context);
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                this.CreateRole(GlobalConstants.AdminRole, context);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdminUsername,
                    FirstName = AdminFirstName,
                    LastName = AdminLastName,
                    Gender = AdminGender,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.UtcNow
                };

                userManager.Create(user, AdminPassword);
                userManager.AddToRole(user.Id, GlobalConstants.AdminRole);
            }
        }

        private void CreateRole(string roleName, MsSqlDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = roleName };

            roleManager.Create(role);
        }
    }
}
