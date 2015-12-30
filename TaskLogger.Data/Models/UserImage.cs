using System;
using System.ComponentModel.DataAnnotations;

namespace TaskLogger.Data.Models
{
    public class UserImage
    {
        [Key]
        public Guid UserImageId { get; set; }
        public byte[] ImageBytes { get; set; }
        public virtual  User  User{ get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
