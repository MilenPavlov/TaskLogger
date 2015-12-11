using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLogger.Data.Models
{
    using TaskLogger.Data.Responses;
    using TaskLogger.Data.Services;

    public class UserTaskEntry 
    {
        [Key]
        public int UserTaskEntryId { get; set; }
        public int UserTaskId { get; set; }
        [Required]
        public decimal UnitsCompleted { get; set; }

        public decimal HoursWorked { get; set; }
        public DateTime DateTimeCompleted { get; set; }

        public string Notes { get; set; }

        public bool Archived { get; set; }
        public virtual UserTask UserTask { get; set; }
    }
}
