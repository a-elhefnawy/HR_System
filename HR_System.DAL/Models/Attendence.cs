using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class Attendence
    {
        public int Id { get; set; }

        [DataType(DataType.Time)]

        public DateTime AttendenceTime { get; set; }
        [DataType(DataType.Time)]

        public DateTime LeavingTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Day { get; set; }

        [ForeignKey("EmoloyeeId")]
        public Employee? Employee { get; set; }
        public int? EmoloyeeId { get; set; }
    }
}
