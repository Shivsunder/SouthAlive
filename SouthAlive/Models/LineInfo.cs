using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SouthAlive.Models
{
    public class LineInfo
    {
        [Key]
        public int LineInfoID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public virtual int PolyLineId { get; set; }
        public virtual Polyline PolyLine { get; set; }
    }
}
