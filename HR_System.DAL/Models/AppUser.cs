﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class AppUser:IdentityUser
    {
        [ForeignKey("Role")]
        public int CategoryId { get; set; }
        public  Role Role { get; set; }
    }
}
