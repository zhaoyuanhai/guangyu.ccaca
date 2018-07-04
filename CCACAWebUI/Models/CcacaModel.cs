using CCACAWebUI.DB;
using System.Collections.Generic;

namespace CCACAWebUI.Models
{
    public class CcacaModel
    {
        public int OneId { get; set; }

        public int TowId { get; set; }

        public int ThreeId { get; set; }

        public List<T_Configure> ConfigList { get; set; }

        public List<T_Company> Company { get; set; }
    }
}
