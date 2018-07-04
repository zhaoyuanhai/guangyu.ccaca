using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CCACAWebUI.TagHelpers
{
    [HtmlTargetElement("p", Attributes = "has-error")]
    public class HasErrorTaghelper : TagHelper
    {
        public string HasError { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(HasError))
            {
                output.Attributes.Add("class", "has-error");
                output.Content.Append(this.HasError);
            }
            else
            {
                output.Attributes.Add("class", "hide");
            }
        }
    }
}
