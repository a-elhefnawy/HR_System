﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class PermissionsDB
    {
        public byte PermissionNumber { get; set; }
        
        public int PageNameId { get; set; }
        public int RoleId { get; set; }
            
        
        public PagesName PageName { get; set; }
        public Role? Role { get; set; }
    }
}
