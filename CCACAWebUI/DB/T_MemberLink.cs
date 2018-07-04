using CCACAWebUI.TagHelpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_MemberLink")]
    public class T_MemberLink : IIcon
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string Icon { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
