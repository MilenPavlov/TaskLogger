using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLogger.Data.Models
{
    public class UserImage
    {
        public Guid UserImageId { get; set; }
        public byte[] ImageBytes { get; set; }
        public virtual  User  User{ get; set; }
    }
}
