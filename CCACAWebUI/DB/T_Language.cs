using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CCACAWebUI.DB
{
    [Table(name: "T_Language")]
    public class T_Language
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Lang { get; set; }
    }
}
