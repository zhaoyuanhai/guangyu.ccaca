using CCACAWebUI.ClassExtends;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CCACAWebUI.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "carousel")]
    public class CarouselTagHelper : TagHelper
    {
        public string Id { get; set; }

        public IEnumerable<CarouselModel> Carousel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("id", this.Id);
            output.Attributes.Add("class", "carousel slide carouse-h");

            XElement indEle = new XElement("ol",
                new XAttribute("class", "carousel-indicators"));
            for (int i = 0; i < this.Carousel.Count(); i++)
            {
                var li = new XElement("li",
                    new XAttribute("data-target", $"#{this.Id}"),
                    new XAttribute("data-slide-to", i));
                if (i == 0)
                    li.SetAttributeValue("class", "active");

                indEle.Add(li);
            }

            XElement carEle = new XElement("div",
                new XAttribute("class", "carousel-inner"));
            this.Carousel.ForEach((i, item) =>
            {
                var div = new XElement("div",
                      new XAttribute("class", i == 0 ? "item active" : "item"));
                div.Add(new XElement("img",
                    new XAttribute("src", item.Path),
                    new XAttribute("alt", item.Name)));
                carEle.Add(div);
            });

            /*
              <div id="myCarousel" class="carousel slide carouse-h">
                <!-- 轮播（Carousel）指标-->
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                    <li data-target="#myCarousel" data-slide-to="3"></li>
                    <li data-target="#myCarousel" data-slide-to="4"></li>
                </ol>
                <!-- 轮播（Carousel）项目 -->
                <div class="carousel-inner">
                    <div class="item active"><img src="~/imgs-test/slider1.jpg" alt="First slide" /></div>
                    <div class="item"><img src="~/imgs-test/slider2.jpg" /></div>
                    <div class="item"><img src="~/imgs-test/slider3.jpg" /></div>
                    <div class="item"><img src="~/imgs-test/slider4.jpg" /></div>
                    <div class="item"><img src="~/imgs-test/slider5.jpg" /></div>
                </div>
                <!-- 轮播（Carousel）导航-->
                <a href="#myCarousel" data-slide="prev" class="carousel-control left">‹</a>
                <a href="#myCarousel" data-slide="next" class="carousel-control right">›</a>
            </div> 
             */

            output.Content.SetHtmlContent(indEle.ToString() +
                carEle.ToString() +
                $"<a href=\"#{this.Id}\" data-slide=\"prev\" class=\"carousel-control left\">‹</a>" +
                $"<a href=\"#{this.Id}\" data-slide=\"next\" class=\"carousel-control right\">›</a>");
        }
    }

    public interface CarouselModel
    {
        string Path { get; set; }

        string Name { get; set; }
    }
}
