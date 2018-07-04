using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_Company")]
    public class T_Company
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Icon { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
