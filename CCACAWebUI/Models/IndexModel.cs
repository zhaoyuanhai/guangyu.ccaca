using CCACAWebUI.DB;
using System.Collections.Generic;

namespace CCACAWebUI.Models
{
    public class IndexModel
    {
        public List<T_Home> ConfigList { get; set; }

        public List<T_Configure> Contact { get; set; }

        public List<T_Carousel> CarouselFigure { get; set; }

        public List<T_ProjectInfo> Project { get; set; }

        public List<T_Information> Infomation { get; set; }

        public List<T_NewActive> NewActive { get; set; }

        public int? QQId { get; set; }

        public int? PhoneId { get; set; }
    }
}
