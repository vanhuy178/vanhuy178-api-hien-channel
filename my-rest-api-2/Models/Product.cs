using System;

namespace my_rest_api_2.Models
{
    public class ProductVM
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
    public class Product : ProductVM 
    {
        public Guid ProductId { get; set; }
    }


}
