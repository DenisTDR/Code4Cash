
using System;
using System.Linq;
using System.Threading.Tasks;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Misc;

namespace Code4Cash.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Code4Cash.Data.Database.Code4CashDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Code4CashDbContext context)
        {
            var seeder = new DatabaseSeeder();
            seeder.Seed();
        }


    }
}