using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_User")]
    public class T_User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }

        public string Email { get; set; }

        public string RealName { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Phone { get; internal set; }

        public bool IsVip { get; set; }
    }
}
