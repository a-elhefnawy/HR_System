using HR_System.DAL.Data;
using HR_System.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Validations
{
    internal class UniqueNationalIdAttribute : ValidationAttribute
    {
        private HRDBContext db = new HRDBContext();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string nationalId = (string)value;

            Employee? employee = db.Employees.FirstOrDefault(e => e.NationalID == nationalId);

            if (employee is not null)
            {
                return new ValidationResult("الرقم القومي مُسجل بالفعل");
            }
            return ValidationResult.Success;
        }
    }
}
