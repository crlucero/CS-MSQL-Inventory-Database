using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class CollectionItemController : Controller
    {
        [HttpGet("/item/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpGet("/item")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost("/item")]
        public ActionResult Create(string ItemDescription)
        {   
            CollectionItem newCollection = new CollectionItem(ItemDescription);
            newCollection.Save();
            List<CollectionItem> allCollections = CollectionItem.GetAll();
            return View("Index", allCollections);
        }
        
        [HttpGet("/item/search")]
        public ActionResult SearchForm()
        {   
            return View();
        }

        [HttpPost("/item/search/result")]
        public ActionResult CreateResult(string name)
        {   
            List<CollectionItem> allCollections = CollectionItem.Find(name);
            return View("Index", allCollections);
        }
        
    }
}
