using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_Member")]
    public class T_Member
    {
        public int ID { get; set; }

        public string Item { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
