using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;


namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            //create a new product object
            Product myProduct = new Product();
            //set the property
            myProduct.Name = "Kayak";
            // get the property
            string productName = myProduct.Name;
            //generate the view
            return View("Result", (object)String.Format("Product name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            //object initializer used to create and populate a new object
            Product myProduct = new Product
            {
                ProductId = 102,
                Name = "Kayak",
                Description = "Kayak for 1 person",
                Price = 25,
                Category = "Infalatable"
            };
            return View("Result", (object)String.Format("The category of  {0} is {1}", myProduct.Name, myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = { "Greece", "Iceland", "Egypt" };
            List<int> intList = new List<int> { 32, 11, 38 };
            var result = string.Join(",", intList.Select(x => x.ToString()).ToArray());
            int myNumber = 7;
            Dictionary<string, int> myDict = new Dictionary<string, int> { { "Greece", 32 }, { "Iceland", 11 }, { "Egypt", 38 } };
            string s = string.Join(",", myDict.Select(x => x.Key + "=" + x.Value).ToArray());

            //return View("Result", (object)stringArray[0]);
            //return View("Result", (object)myNumber.ToString());
            //return View("Result", (object)s);
            return View("Result", (object)result);
        }
        public ViewResult UseExtension()
        {
            ShoppingCart myCart = new ShoppingCart()
            {
                Products = new List<Product>
                {
                    new Product { Name = "K1", Price = 23.00M },
                    new Product { Name = "K2", Price = 26.00M },
                    new Product { Name = "K3", Price = 29.00M }
                }
            };
            decimal cartTotal = myCart.TotalPrices();
            //return View("Result", (object)System.Convert.ToString(cartTotal));
            return View("Result", (object)String.Format("My shopping cart total is {0}", cartTotal)); 
        }
    }
}