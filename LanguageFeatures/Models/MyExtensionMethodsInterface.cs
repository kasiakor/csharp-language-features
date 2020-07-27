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

        //filtering method to filter collection of objects
        //category parameter added to inject a filter condition
        //yeld keyword to apply selection criteria to items in the source data
        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product> productEnum, string categoryParam)
        {
            foreach (Product product in productEnum)
            {
               if(product.Category == categoryParam)
                {
                    //total is calculated in the controller with filtering applied to foreach loop on products
                    yield return product;
                }
            }
        }
    }
}