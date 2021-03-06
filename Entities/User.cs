﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; }
        public DateTime RegDate { get; set; }
        public int Messages { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
        public bool IsBaned { get; set; }
    }
}
