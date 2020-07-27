using System.Collections;
using System.Collections.Generic;

namespace LanguageFeatures.Models
{
    public class ShoppingCartInterface : IEnumerable<Product>
    {
        public List<Product> Products { get;  set;  }

        public IEnumerator<Product> GetEnumerator()
        {
            return Products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //returns IEnumerator object to iterate through a collection
            return GetEnumerator();
        }
    }
}