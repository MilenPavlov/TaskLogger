using System;

namespace TaskLogger.Data.Models
{
    public class UserImage
    {
        public Guid UserImageId { get; set; }
        public byte[] ImageBytes { get; set; }
        public virtual  User  User{ get; set; }
        public string UserId { get; set; }
    }
}
