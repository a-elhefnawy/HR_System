using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class GeneralSittings
    {
        [Key]
        public int sittings_Id { get; set; }
        public int overTime { get; set; }
        public int underTime { get; set; }
        public string week_end_Day1 { get; set; }
        public string week_end_Day2 { get; set; }

        public DateTime date { get; set; }= DateTime.Now;
    }
}
