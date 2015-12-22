using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Context
{
    public class TaskLoggerContext : IdentityDbContext<User>
    {
        public TaskLoggerContext()
            : base("TaskLogger", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            
        }


        public static TaskLoggerContext Create()
        {
            return new TaskLoggerContext();
        }

        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<UserTaskEntry> UserTaskEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTask>()
                .HasRequired(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserTaskEntry>()
                .HasRequired(x => x.UserTask)
                .WithMany()
                .HasForeignKey(x => x.UserTaskId);

            modelBuilder.Entity<User>().Ignore(x => x.Age);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.UserImage)
                .WithRequired(ui => ui.User);
        }
    }
}
