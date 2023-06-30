using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Validations
{
    internal class AgeGreaterThanOrEqual20Attribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime birthDate = (DateTime)value;
            DateTime now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now < birthDate.AddYears(age))
            {
                age--;
            }
            if (age < 20)
            {
                return new ValidationResult("يجب ألا يقل عمر الموظف عن 20 سنة");
            }
            return ValidationResult.Success;
        }
    }
}
