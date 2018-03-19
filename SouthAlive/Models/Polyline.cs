using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SouthAlive.Models
{
    public class Polyline : IComparable<Polyline>
    {
        [Key]
        public int PolylineId { get; set; }
        public string PolyLine { get; set; }
        public string Notes { get; set; } //Intended for Admin Use only.
        public DateTime DateAdded { get; set; }
        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime LastChecked { get; set; }

        public virtual LineInfo LineInfo { get; set; }

        public string Dslc
        {
            get
            {
                TimeSpan t = (DateTime.Now - this.LastChecked);
                if (t.Days < 1)
                {
                    if (t.Hours == 1)
                    {
                        return "1 hour ago";
                    }
                    else
                    {
                        return t.Hours + " hours ago";
                    }
                }
                else
                {
                    int d = t.Days;
                    if (d > 730) { return (d / 365) + " years ago"; }
                    else if (d > 365) { return "1 year ago"; }
                    else if (d > 62) { return (d / 31) + " months ago"; }
                    else if (d > 30) { return "1 month ago"; }
                    else if (d > 1) { return d + " days ago"; }
                    else { return "1 days ago"; }
                }
            }
        }
        public int CompareTo(Polyline line)
        {
            //return this.LineInfo.Name.CompareTo(line.LineInfo.Name);
            //return this.LastChecked.Day/0;
            return this.LastChecked.CompareTo(line.LastChecked);
            //return (int)(this.LastChecked.Minute-line.LastChecked.Minute);
        }
    }
}
