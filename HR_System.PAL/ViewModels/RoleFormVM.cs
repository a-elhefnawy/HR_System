﻿using System.ComponentModel.DataAnnotations;

namespace HR_System.ViewModel
{
    public class RoleFormVM
    {
        [Required , StringLength(150)]
        public string Name { get; set; }
    }
}
