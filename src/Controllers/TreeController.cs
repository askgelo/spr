using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Services;
using Microsoft.AspNetCore.Http;
using src.Models;
using src.App_Code;
using Microsoft.AspNetCore.Html;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace src.Controllers
{
    public class TreeController : Controller
    {
        ITreeService treeService;

        public TreeController(ITreeService treeService)
        {
            this.treeService = treeService;
        }

        public IActionResult Index()
        {
            var tree = GetTree();
            if (tree == null) return RedirectToAction("Index", "Home");
            return View(tree);
        }

        public IActionResult Node(string path)
        {
            var tree = GetTree();
            if (tree != null)
            {
                if (!string.IsNullOrWhiteSpace(path) && tree.IsExists(path))
                {
                    tree.SetActive(path);
                    return PartialView("_NodeData", tree.Active);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult Add()
        {
            var tree = GetTree();
            if (tree != null)
            {
                // model
                var newNode = Models.Node.Create("new");
                tree.Active.Add(newNode);

                // view
                HtmlString nodeTree;
                if (tree.Active.Count == 1) // new root
                    nodeTree = TagToHtmlString.Create(TreeHelper.CreateNode(tree.Active));
                else
                    nodeTree = TagToHtmlString.Create(TreeHelper.CreateNode(newNode));

                var nodeList = ListHelper.CreateElement(newNode);
                return Json(new { nodeTree = nodeTree.Value, nodeList = nodeList.Value });
            }
            // ??
            return Json(new { tree = string.Empty });
        }

        Node GetTree()
        {
            if (HttpContext.Session.Keys.Contains("dwp"))
            {
                string dwp = HttpContext.Session.GetString("dwp");
                return treeService.GetTree(dwp);
            }
            else return null;
        }
    }
}