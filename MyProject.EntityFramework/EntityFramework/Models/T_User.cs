using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_User")]
    public class T_User
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string Email { get; set; }

        public string RealName { get; set; }

        public string Phone { get; set; }

        public bool IsVip { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
