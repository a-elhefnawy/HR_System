using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.PAL.ViewModels
{
    public class GeneralSittingsVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage="هذا الحقل مطلوب")]
        public int overTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int underTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string week_end_Day1 { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string week_end_Day2 { get; set; }

        public DateTime date { get; set; }= DateTime.Now;
    }
}
