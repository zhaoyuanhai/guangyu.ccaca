using CCACAWebUI.DB;
using System.Collections.Generic;

namespace CCACAWebUI.Models
{
    public class ContactModel
    {
        public List<T_Configure> ConfigList { get; set; }
        public T_Carousel RqCode { get; set; }
    }
}
