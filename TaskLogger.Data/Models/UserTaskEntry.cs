using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLogger.Data.Models
{
    public class UserTaskEntry
    {
        public int UserTaskEntryId { get; set; }
        public int UserTaskId { get; set; }
        public decimal UnitsCompleted { get; set; }
        public DateTime DateTimeCompleted { get; set; }

        public virtual UserTask UserTask { get; set; }
    }
}
