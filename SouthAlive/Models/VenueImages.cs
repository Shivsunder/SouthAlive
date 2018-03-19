using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SouthAlive.Models
{
    public class VenueImages
    {
        [Key]
        public int VenueImagesID { get; set; }

        public string ImagePath { get; set; }

        public virtual int VenueID { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
