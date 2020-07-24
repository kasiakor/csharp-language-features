using LanguageFeatures.Models;
using System;
using System.Web.Mvc;

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
                Category="Infalatable"
            };
            return View("Result", (object)String.Format("The category of  {0} is {1}", myProduct.Name, myProduct.Category));
        }
    }
}