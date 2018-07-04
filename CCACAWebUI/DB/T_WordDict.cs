using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_WordDict")]
    public class T_WordDict
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Word { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int LanguageId { get; set; }
    }
}
