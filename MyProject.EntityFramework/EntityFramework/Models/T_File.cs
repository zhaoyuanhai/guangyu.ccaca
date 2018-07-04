using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_File")]
    public class T_File
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        //public int TypeId { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
