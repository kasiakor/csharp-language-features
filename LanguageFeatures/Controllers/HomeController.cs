using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Text;

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
            //int myNumber = 7;
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

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCartInterface()
            {
                Products = new List<Product>
                {
                    new Product { Name = "K1", Price = 23.00M },
                    new Product { Name = "K2", Price = 26.00M },
                    new Product { Name = "K3", Price = 29.00M }
                }
            };

            //create and populate an array of Product objects
            Product[] productArray = {
                new Product { Name = "K1", Price = 23.00M },
                    new Product { Name = "K2", Price = 26.00M },
                    new Product { Name = "K3", Price = 29.00M }
            };

            //extension method in use - TotalPrice()
            //due to switch to interface we can use it:
            //1. for objects enumerated by IEnumerable<Product>, incl instances of ShoppingCart
            //2. for array of Products
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();

            return View("Result", (object)String.Format("My shopping cart total is {0} that should equal to total from the array of products {1}", cartTotal, arrayTotal));
        }
        public ViewResult UseFilterExtension()
        {
            IEnumerable<Product> products = new ShoppingCartInterface()
            {
                Products = new List<Product>
                {
                    new Product { Name = "K1", Price = 23.00M, Category = "River" },
                    new Product { Name = "K2", Price = 26.00M, Category = "River" },
                    new Product { Name = "K3", Price = 29.00M, Category = "Lake" }
                }
            };
            decimal totalFilter = 0;
            //extension method that filteres the collection, FilterByCategory
            foreach (Product product in products.FilterByCategory("Lake"))
            {
                totalFilter += product.Price;
            }

            return View("Result", (object)String.Format("My shopping cart total with filter is {0}", totalFilter));
        }

        public ViewResult UseFilterExtensionFunc()
        {
            IEnumerable<Product> products = new ShoppingCartInterface()
            {
                Products = new List<Product>
                {
                    new Product { Name = "K1", Price = 25.00M, Category = "River" },
                    new Product { Name = "K2", Price = 26.00M, Category = "River" },
                    new Product { Name = "K3", Price = 29.00M, Category = "Lake" }
                }
            };

            //delegate Func
            // it can point to any method, as long as the values, a method accepts and returns, match the delegate signature
            //using lambda expression
            Func<Product, bool> categoryFilter = product => product.Category == "River";
            decimal totalFilter = 0;
            //extension method that filteres the collection
            foreach (Product product in products.Filter(categoryFilter))
            {
                totalFilter += product.Price;
            }

            return View("Result", (object)String.Format("My shopping cart total with filter and Func is {0}", totalFilter));
        }

        public ViewResult UseFilterExtensionNoFunc()
        {
            IEnumerable<Product> products = new ShoppingCartInterface()
            {
                Products = new List<Product>
                {
                    new Product { Name = "K1", Price = 25.00M, Category = "River" },
                    new Product { Name = "K2", Price = 11.00M, Category = "Estuary" },
                    new Product { Name = "K3", Price = 29.00M, Category = "Lake" }
                }
            };

            // it can point to any method, as long as the values, a method accepts and returns, match the delegate signature
            //using lambda expression without Func
            decimal totalFilter = 0;
            //extension method that filteres the collection
            foreach (Product product in products.Filter(product => product.Category == "Estuary"))
            {
                totalFilter += product.Price;
            }

            return View("Result", (object)String.Format("My shopping cart total with filter without Func is {0}", totalFilter));
        }

        public ViewResult UseFilterExtensionNoFuncExtended()
        {
            IEnumerable<Product> products = new ShoppingCartInterface()
            {
                Products = new List<Product>
                {
                    new Product { Name = "K1", Price = 25.00M, Category = "River" },
                    new Product { Name = "K2", Price = 11.00M, Category = "Estuary" },
                    new Product { Name = "K3", Price = 29.00M, Category = "Lake" },
                    new Product { Name = "K4", Price = 31.00M, Category = "Lake" },
                    new Product { Name = "K5", Price = 33.00M, Category = "Lake" }
                }
            };

            // it can point to any method, as long as the values, a method accepts and returns, match the delegate signature
            //using lambda expression without Func
            decimal totalFilter = 0;
            //extension method that filteres the collection
            foreach (Product product in products.Filter(product => product.Category == "Estuary" || product.Price > 30.00M))
            {
                totalFilter += product.Price;
            }

            return View("Result", (object)String.Format("My shopping cart total with filter without Func, extended, is {0}", totalFilter));
        }

        public ViewResult CreateAnonArray()
        {

            ////type inference, local variable without explicitly specifying the type
            //comipler allows only members of inferred class
            //var myVariable = new Product { Name = "B1", Price = 25.00M, Category = "Road" };
            //string name = myVariable.Name;
            //decimal val = myVariable.Price;
            //string category = myVariable.Category;
            //int count = myVariable.Count //compiler error

            //create simply object without defining the class using type inference
            //anonymously typed object
            //get set only properties defined in the initializer
            //var myAnonType = new
            //{
            //    Name = "B2",
            //    Category = "Mountain"
            //};

            var oddsAndEnds = new[]
            {
                new { Name = "Table", Category = "Furniture"},
                new { Name = "Ball", Category = "Basketball"},
                new { Name = "Dinghy", Category = "Sailing"}
            };

            //Represents a mutable string of characters. This class cannot be inherited.
            StringBuilder result = new StringBuilder();
            foreach(var item in oddsAndEnds)
            {
                result.Append("Name: " + item.Name).Append(", ").Append("Category: " + item.Category).Append("; ");
            }

            return View("Result", (object)result.ToString());
        }
    }
}