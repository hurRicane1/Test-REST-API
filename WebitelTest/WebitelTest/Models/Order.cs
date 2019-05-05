using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebitelTest.Models
{
    [Table("Order")]
    public class Order
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public IList<OrderProduct> Products { get; set; }
        public Order()
        {
            Products = new List<OrderProduct>();
        }
    }
}
