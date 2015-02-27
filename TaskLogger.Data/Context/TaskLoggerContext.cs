using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Context
{
    public class TaskLoggerContext : IdentityDbContext<User>
    {
        public TaskLoggerContext(): base("TaskLoggerDb")
        {          
        }

        //public DbSet<User> Users { get; set; } 
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


        }
    }
}
