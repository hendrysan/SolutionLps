using System.Collections.Generic;

namespace Solution.LogicalCode
{
    class Product
    {
        public Product(string sku, decimal price)
        {
            SKU = sku;
            Price = price;
        }
        public string SKU { get; set; }
        public decimal Price { get; set; }
    }

    public class MemoryLeakFirst
    {
        public long Main()
        {
            //var myList = new List(); // worng code
            var myList = new List<Product>(); // correct code   


            //while (true) // wrong code, looping forever
            //{
            // populate list with 1000 integers
            for (int i = 0; i < 1000; i++)
            {
                myList.Add(new Product(Guid.NewGuid().ToString(), i));
            }
            // do something with the list object
            Console.WriteLine(myList.Count);
            //}

            return myList.LongCount();
        }
    }
}
