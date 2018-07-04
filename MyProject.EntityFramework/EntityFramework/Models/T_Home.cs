using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_Home")]
    public class T_Home
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Cover { get; set; }
    }
}
