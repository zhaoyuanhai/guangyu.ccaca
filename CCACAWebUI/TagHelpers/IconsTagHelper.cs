using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CCACAWebUI.TagHelpers
{
    [HtmlTargetElement(Attributes = "icons")]
    public class IconsTagHelper : TagHelper
    {
        public IEnumerable<IIcon> Icons { get; set; }

        public int PageCount { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var icons = Icons.ToList();
            for (var i = 0; i < Math.Ceiling(icons.Count * 1.0 / PageCount); i++)
            {
                XElement ulEle = new XElement("ul",
                    new XAttribute("class", "links current clear"));
                for (int x = i * PageCount; x < ((i + 1) * PageCount) && x < icons.Count; x++)
                {
                    var item = icons[x];
                    ulEle.Add(new XElement("li",
                            new XElement("a",
                               new XAttribute("href", item.Link),
                               new XAttribute("target", "_blank"),
                               new XElement("img",
                                   new XAttribute("src", item.Icon),
                                   new XAttribute("alt", item.Name)))));
                }
                output.Content.AppendHtml(ulEle.ToString());
            }
        }
    }

    public interface IIcon
    {
        string Link { get; set; }

        string Icon { get; set; }

        string Name { get; set; }
    }
}
