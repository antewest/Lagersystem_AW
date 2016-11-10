
using LagerSystem1.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LagerSystem1.Models;

namespace LagerSystem1.Repositories
{
    public class StoreRepository
    {
        private StoreContext _db = new StoreContext();
        private IEnumerable<StockItem> _current;

        

        public List<StockItem> GetList()
        {
            return _db.Items.ToList();
        }

        internal StockItem GetItem(int id)
        {
            return _db.Items.Find(id);
        }

        internal void Add(StockItem item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
        }

        internal void Edit(StockItem item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        internal void Remove(StockItem item)
        {
            _db.Items.Remove(item);
            _db.SaveChanges();
        }

        internal List<StockItem> Search(string input)
        {
            var results = _db.Items.Where(b => input == null || b.Name.Contains(input) || b.Shelf.Contains(input) || b.Description.Contains(input)).ToList();
            
            return results;
        }

        internal List<StockItem> SortBy(string input, bool sortByAsc, List<StockItem> existingResult = null)
        {
            IEnumerable<StockItem> results;

            if (existingResult != null)
                results = existingResult;
            else
                results = GetList();

            if (sortByAsc)
                return results.OrderBy(b => b.GetType().GetProperty(input).GetValue(b, null)).ToList();
            else
                return results.OrderByDescending(b => b.GetType().GetProperty(input).GetValue(b, null)).ToList();
               
        }

        internal void Undo(string name, int price, string shelf, string desc)
        {
            StockItem item = new StockItem {Name = name, Price = price, Shelf = shelf, Description = desc};
            Add(item);
        }
    }
}