using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskLogger.Data.Models
{
    public class UserImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserImageId { get; set; }
        public byte[] ImageBytes { get; set; }
        public virtual  User  User{ get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
