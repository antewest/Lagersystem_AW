using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LagerSystem1.Helpers
{
    public static class Helper
    {
        

        public static object GetPropertyValue(string input, object src)
        {
            return src.GetType().GetProperty(input).GetValue(src, null);
        }
    }
}