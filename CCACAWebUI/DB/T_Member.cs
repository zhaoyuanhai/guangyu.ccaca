using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace CCACAWebUI.DB
{
    [Table(name: "T_Member")]
    public class T_Member
    {
        public int ID { get; set; }

        public string Item { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
