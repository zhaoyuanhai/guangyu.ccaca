using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_Ref")]
    public class T_Ref
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public string FiledName { get; set; }

        [Required]
        public int RowID { get; set; }

        [Required]
        public string RowValue { get; set; }

        [Required]
        public int LanguageID { get; set; }
    }
}
