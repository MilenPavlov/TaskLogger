using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLogger.Data.Models
{
    public class UserTask
    {
        [Key]
        public int UserTaskId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual User User { get; set; }
    }
}
