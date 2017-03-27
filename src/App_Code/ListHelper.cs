using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace src.App_Code
{
    public static class ListHelper
    {
        //public static HtmlString CreateList(this IHtmlHelper html, Node node)
        //{
        //    return GetList(node);
        //}

        public static HtmlString CreateElement(this IHtmlHelper html, Node node)
        {
            return CreateElement(node);
        }

        public static HtmlString CreateElement(Node node)
        {
            var tagElement = new TagBuilder("div");
            tagElement.AddCssClass("element-node");
            tagElement.Attributes.Add("key", node.GetPath());
            tagElement.InnerHtml.Append(node.Name);

            return TagToHtmlString.Create(tagElement);
        }

        //internal static HtmlString GetList(Node node)
        //{
        //    TagBuilder tag = new TagBuilder("div");
        //    tag.AddCssClass("div-list-nodes");
        //    tag.Attributes.Add("id", "");
        //    tag.Attributes.Add("style", "overflow-y: auto");
        //    foreach (var element in node)
        //    {
        //        AddElement(tag, element);
        //    }
        //    return TagToHtmlString.Create(tag);
        //}

        //static void AddElement(TagBuilder tagBuilder, Node element)
        //{
        //    var tagElement = new TagBuilder("div");
        //    tagElement.AddCssClass("element-node");
        //    tagElement.Attributes.Add("style", "display: inline-block; width: 120px");
        //    tagElement.Attributes.Add("key", element.GetPath());
        //    tagElement.InnerHtml.Append(element.Name);

        //    tagBuilder.InnerHtml.AppendHtml(tagElement);
        //}
    }
}
