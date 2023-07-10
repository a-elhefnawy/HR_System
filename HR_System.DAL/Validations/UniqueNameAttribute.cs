using HR_System.DAL.Data;
using HR_System.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Validations
{
    internal class UniqueNameAttribute : ValidationAttribute
    {
        private HRDBContext context = new HRDBContext();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? departmentName = (string?)value;

            Department? departmentFromRequest = validationContext.ObjectInstance as Department;


            Department? departmentFromDB = context.Departments.FirstOrDefault(dept => dept.Name.ToLower() == departmentName.ToLower());

            if (departmentFromDB is not null)
                return new ValidationResult("هذا القسم موجود بالفعل");
            
            return ValidationResult.Success;
        }
    }
}
