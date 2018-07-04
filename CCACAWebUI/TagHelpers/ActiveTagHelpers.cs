using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CCACAWebUI.TagHelpers
{
    [HtmlTargetElement(Attributes = "active-expre")]
    public class ActiveTagHelpers : TagHelper
    {
        public bool activeExpre { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagHelperAttribute attr;
            string classValue = string.Empty;
            output.Attributes.TryGetAttribute("class", out attr);
            if (attr != null)
            {
                classValue = attr.Value.ToString();
            }
            //else {
            //    classValue = "active";
            //}
            if (activeExpre)
            {
                output.Attributes.SetAttribute("class", classValue + " active");
            }
        }
    }
}
