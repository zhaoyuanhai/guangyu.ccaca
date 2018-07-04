using CCACAWebUI.DB;
using System.Collections.Generic;

namespace CCACAWebUI.Models
{
    public class VipModel
    {
        public List<T_MemberLink> MemberLink { get; set; }

        public List<T_Member> Member { get; set; }

        public T_Carousel Carousel { get; set; }
    }
}
