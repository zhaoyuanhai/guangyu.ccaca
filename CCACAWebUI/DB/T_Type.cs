using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_Type")]
    public class T_Type
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
