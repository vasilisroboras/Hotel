using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Hotel.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    
    
}


