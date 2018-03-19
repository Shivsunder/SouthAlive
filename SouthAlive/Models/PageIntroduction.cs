using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SouthAlive.Models
{
    public class PageIntroduction
    {
        public int Id { get; set; }
        [Column(TypeName = "text")]
        public string Text { get; set; }
    }
}
