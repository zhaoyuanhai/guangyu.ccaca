using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace MyProject.EntityFramework.Models
{
    [Table(name: "T_Carousel")]
    public class T_Carousel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }
    }
}
