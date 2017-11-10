using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTest
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            MigrateDbToLastestVersion();
            Seed();
        }

        private static void MigrateDbToLastestVersion()
        {
            var configuration = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();
            if (context.Users.Any())
                return;
            context.Users.Add(new ApplicationUser { UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-" });
            context.Users.Add(new ApplicationUser { UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-" });
            context.SaveChanges();
        }
    }
}
