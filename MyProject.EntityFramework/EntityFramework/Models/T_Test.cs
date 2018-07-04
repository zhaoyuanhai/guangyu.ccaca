using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_Test")]
    public class T_Test
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
