using CCACAWebUI.TagHelpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCACAWebUI.DB
{
    [Table(name: "T_Carousel")]
    public class T_Carousel : CarouselModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }
    }
}
