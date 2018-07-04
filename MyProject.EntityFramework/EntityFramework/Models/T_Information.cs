using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_Information")]
    public class T_Information
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Conver { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
