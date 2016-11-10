using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LagerSystem1.Models;

namespace LagerSystem1.ViewModels
{
    public class ResultModel
    {

        //Contains relevant result, filtered or not
        public List<StockItem> results;
        //Search term if used
        public string query = "";
        public StockItem item;

        //Decides sorting direction of categories
        public Dictionary<string, bool> sortStates = new Dictionary<string, bool>()
        {
            {"Name", false},
            {"Price", false},
            {"Shelf", false}
        };
    }
}