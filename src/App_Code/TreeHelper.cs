using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Models;
using System.IO;
using System.Text.Encodings.Web;
using System;

namespace src.App_Code
{
    public static class TreeHelper
    {
        public static HtmlString CreateTree(this IHtmlHelper html, Node tree)
        {
            var ul = CreateTree(tree);
            return TagToHtmlString.Create(ul);
        }

        internal static TagBuilder CreateNode(Node node)
        {
            var li = new TagBuilder("li");

            var divElement = new TagBuilder("div");
            divElement.AddCssClass("nav-header element-tree");
            //divElement.Attributes.Add("style", "text-align: justify; padding: 7px 0 7px 0; cursor: pointer");
            divElement.Attributes.Add("id", node.GetPath());

            if (node.Count > 0)
            {
                var toggler = new TagBuilder("span");
                toggler.AddCssClass("glyphicon glyphicon-menu-right tree-toggler");
                divElement.InnerHtml.AppendHtml(toggler);
            }

            var text = new TagBuilder("span");
            if (node.Count > 0) text.AddCssClass("node-children");
            else text.AddCssClass("node-simple");
            //if (node.Count > 0) text.Attributes.Add("style", "padding: 0 0 0 7px");
            //else text.Attributes.Add("style", "padding: 0 0 0 21px");
            text.InnerHtml.Append(node.Name);
            divElement.InnerHtml.AppendHtml(text);

            li.InnerHtml.AppendHtml(divElement);

            if (node.Count > 0)
            {
                var child = CreateTree(node);
                child.Attributes.Add("style", "display:none");
                li.InnerHtml.AppendHtml(child);
            }
            return li;
        }

        static TagBuilder CreateTree(Node tree)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("nav nav-list tree");
            foreach (var node in tree)
            {
                ul.InnerHtml.AppendHtml(CreateNode(node));
            }
            return ul;
        }
    }
}
