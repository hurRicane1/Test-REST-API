using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebitelTest.Models
{
    [Table("Product")]
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IList<OrderProduct> Orders { get; set; }
        public Product()
        {
            Orders = new List<OrderProduct>();
        }
    }
}
