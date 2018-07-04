using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_Home")]
    public class T_Home
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Cover { get; set; }
    }
}
