using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace CCACAWebUI.TagHelpers
{
    [HtmlTargetElement("dict", Attributes = "route-data")]
    public class DictTagHelper : TagHelper
    {
        public RouteData RouteData { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
        }
    }
}
