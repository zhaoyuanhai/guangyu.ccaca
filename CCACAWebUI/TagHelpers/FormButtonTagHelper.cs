using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CCACAWebUI.TagHelpers
{
    [HtmlTargetElement("formbutton")]
    public class FormButtonTagHelper : TagHelper
    {
        public string Type { get; set; } = "submit";

        public string BgColor { get; set; } = "primary";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("type", Type);
            output.Attributes.SetAttribute("class", $"btn btn-{BgColor}");
            output.Content.SetContent(Type == "submit" ? "add" : "reset");
        }
    }
}
