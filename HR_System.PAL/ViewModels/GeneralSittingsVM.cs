using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.PAL.ViewModels
{
    public class GeneralSittingsVM
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage="هذا الحقل مطلوب")]
        public int overTime { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int underTime { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string week_end_Day1 { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string week_end_Day2 { get; set; }

        public DateTime date { get; set; }= DateTime.Now;
    }
}
