using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SouthAlive.Models.SampleEditor
{
    public class Editor
    {
        public int EditorId { get; set; }

        public string Title { get; set; }
        [Column(TypeName = "text")]
        public string Content { get; set; }
    }
}
