using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CCACAWebUI.ClassExtends
{
    public static class ExtendsHelper
    {
        /// <summary>
        /// 扩展IEnumerable使其可以直接使用ForEach语法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> self, Action<int, T> action)
        {
            int i = 0;
            foreach (var item in self)
                action(i++, item);
        }

        /// <summary>
        /// 将字符串转换为html字符串
        /// </summary>
        /// <param name="html"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static HtmlString String(this IHtmlHelper html, string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return HtmlString.Empty;
            return new HtmlString(str);
        }

        /// <summary>
        /// 获取Html字符串中的文本，即去掉所有html标签
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string InnerText(this IHtmlHelper htmlHelper, string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            Regex r = new Regex("<[^>]*>");
            string temp = r.Replace(html, string.Empty);
            return temp;
        }
    }
}
