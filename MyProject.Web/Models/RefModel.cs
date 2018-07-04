using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Web.Models
{
    public class RefModel
    {
        public string TableName { get; set; }

        public int LanguageId { get; set; }

        public int Id { get; set; }

        public Dictionary<string,string> Props { get; set; }
    }
}