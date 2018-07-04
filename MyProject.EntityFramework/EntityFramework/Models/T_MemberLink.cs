using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_MemberLink")]
    public class T_MemberLink
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Icon { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
