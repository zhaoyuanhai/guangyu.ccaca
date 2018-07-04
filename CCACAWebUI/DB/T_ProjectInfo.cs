using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_ProjectInfo")]
    public class T_ProjectInfo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? CreatTime { get; set; }
    }
}
