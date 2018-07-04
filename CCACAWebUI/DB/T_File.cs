using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_File")]
    public class T_File
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        //public int TypeId { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
