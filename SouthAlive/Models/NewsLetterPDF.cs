using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SouthAlive.Models
{
    public class NewsLetterPDF
    {
        [Key]
        public int NewsLetterPDFID { get; set; }
        public string Title { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
