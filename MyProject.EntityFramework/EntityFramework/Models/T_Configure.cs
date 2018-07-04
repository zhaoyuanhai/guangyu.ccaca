using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_Configure")]
    public class T_Configure
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Type { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
