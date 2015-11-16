using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using TaskLogger.Data.Context;

namespace TaskLogger.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<TaskLoggerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new SqlServerMigrationSqlGenerator());
            ContextKey = "TaskLogger.Data.Context.TaskLoggerContext";
        }

        protected override void Seed(TaskLoggerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}