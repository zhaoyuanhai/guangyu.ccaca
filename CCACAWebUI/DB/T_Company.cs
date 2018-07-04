using CCACAWebUI.TagHelpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_Company")]
    public class T_Company : IIcon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Icon { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
