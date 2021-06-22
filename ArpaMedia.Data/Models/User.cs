using System;
using System.Collections.Generic;

#nullable disable

namespace ArpaMedia.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordKey { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}
