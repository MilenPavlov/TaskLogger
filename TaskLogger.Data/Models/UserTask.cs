using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;

namespace TaskLogger.Data.Models
{
    public class UserTask 
    {
        [Key]
        public int UserTaskId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public DateTime DateCreated { get; set; }

        public bool Archived { get; set; }
        public virtual User User { get; set; }
    }
}
