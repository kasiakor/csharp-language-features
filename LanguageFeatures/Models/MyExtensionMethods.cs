using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        //this extension method
        //cartParam instance of the Shopping cart
        public static decimal TotalPrices(this ShoppingCart cartParam)
        {
            decimal total = 0;
            foreach (Product product  in cartParam.Products)
            {
                total += product.Price;
            }
            return total;
        }
    }
}