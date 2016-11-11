using LagerSystem1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LagerSystem1.Models;
using LagerSystem1.ViewModels;

namespace LagerSystem1.Controllers
{
    public class NavController :Controller
    {
        private StoreRepository _rep = new StoreRepository();

        public ActionResult Index(string query = null, string sortOrder = null, bool direction = true, string sortInput = null, string undo = null)
        {
            //create viewmodel
            ResultModel results = new ResultModel();

            //assign updated values to viewmodel & populate results
            //if query is null all items will be shown
            results.query = query;
            
            results.results = _rep.Search(query);

            if (undo != null)
                results.item = Converter(undo);

            //Sort buttons send a sortInput value
            if (sortInput != null)
            {
                //Save current sort order and perform sort
                results.sortStates[sortInput] = direction;
                results.results = _rep.SortBy(sortInput, direction, results.results);

                //Reverse sort order
                if (results.sortStates[sortInput])
                    results.sortStates[sortInput] = false;
                else
                    results.sortStates[sortInput] = true;
            }

            return View(results);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StockItem item)
        {
            if (ModelState.IsValid)
            {
                _rep.Add(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _rep.GetItem(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StockItem item)
        {
            if (ModelState.IsValid)
            {
                _rep.Edit(item);
                return RedirectToAction("Index");
            }
            
            return View(item);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _rep.GetItem(id);

            //ResultModel result = new ResultModel {item = new StockItem{Description = model.Description, Shelf = model.Shelf, Name = model.Name, Price = model.Price}};
            ResultModel result = new ResultModel { item = model};

            _rep.Remove(model);

            return RedirectToAction("Index", new {undo = model.ToString()});
        }

        
        public ActionResult Undo(string input)
        {
            StockItem item = Converter(input);
            _rep.Add(item);

            return RedirectToAction("Index");
        }

        private StockItem Converter(string input)
        {
            StockItem item = new StockItem();

            string[] arr = new string[4];
            int index = 0;

            for (int i = 0; i < 3; i++)
            {
                int nextIndex = input.IndexOf('¤', index);
                arr[i] = input.Substring(index, nextIndex-index);
                index = nextIndex + 1;
            }

            arr[3] = input.Substring(index);

            item.Name = arr[0];

            decimal temp = 0;
            Decimal.TryParse(arr[1], out temp);

            item.Price = temp;
            item.Shelf = arr[2];
            item.Description = arr[3];

            return item;
        }

        //************** OLD METHODS, kept for reference************************

        //public ActionResult Index(string query = null, bool useModel = false)
        //{
        //    object model;

        //    if (useModel)
        //    {
        //        model = Session["model"];
        //    }
        //    else
        //    {
        //        model = _rep.Search(query);
        //        Session["_current"] = model;
        //        ResetSortStates();  
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Search(string input)
        //{
        //    var model = _rep.Search(input);

        //    Session["_current"] = model;

        //    return RedirectToAction("Index", model);
        //}

        //[HttpGet]
        //public ActionResult SortBy(string input)
        //{

        //    bool sortOrder = ((Dictionary<string, bool>) Session["_sortStates"])[input];

        //    if (Session["_current"] != null)
        //        Session["model"] = _rep.SortBy(input, sortOrder, Session["_current"]);    
        //    else
        //        Session["model"] = _rep.SortBy(input, sortOrder);

        //    if (((Dictionary<string, bool>)Session["_sortStates"])[input])
        //        ((Dictionary<string, bool>)Session["_sortStates"])[input] = false;
        //    else
        //        ((Dictionary<string, bool>)Session["_sortStates"])[input] = true;

            
        //    return RedirectToAction("Index", new { useModel = true});
        //}

        //[HttpGet]
        //public ActionResult SortBy(string input)
        //{
        //    Dictionary<string, bool> info = (Dictionary<string, bool>)Session["_sortStates"];

        //    List<StockItem> model = _rep.SortBy(input, info[input]);

        //    if (info[input])
        //        ((Dictionary<string, bool>)Session["_sortStates"])[input] = false;
        //    else
        //        ((Dictionary<string, bool>)Session["_sortStates"])[input] = true;

        //    return RedirectToAction("Index", new { list = model });
        //}
    }
}