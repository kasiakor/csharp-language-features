using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethodsInterface
    {
        //this extension method
        //foreach works directly on Product objects
        public static decimal TotalPrices(this IEnumerable<Product> productEnum)
        {
            decimal total = 0;
            foreach (Product product  in productEnum)
            {
                total += product.Price;
            }
            return total;
        }
    }
}