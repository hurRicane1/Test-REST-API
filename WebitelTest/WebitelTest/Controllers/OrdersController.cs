using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebitelTest.Models;
using WebitelTest.Data;
using WebitelTest.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebitelTest.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly DataContext db;
        public OrdersController(DataContext context)
        {
            db = context;
        }
        // GET: api/<controller>
        public JsonResult GetAll()
        {
            var orders = db.Orders.ToList();
            if (orders.Count <= 0)
            {
                return Json(NotFound(Helper.noOrders));
            }
            return Json(Ok(orders));
        }

        // POST api/<controller>
        [HttpPost]
        public JsonResult Post([FromBody]Order order)
        {
            if (order == null)
            {
                return Json(Conflict(Helper.wrongOrder));
            }
            db.Orders.Add(order);
            db.SaveChanges();
            return Json(Ok(order));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{Id}")]
        public JsonResult Delete(Guid Id)
        {
            var order = db.Orders.Where(o => o.Id == Id).FirstOrDefault();
            if (order == null)
            {
                return Json(NotFound(Helper.orderNotFound));
            }
            db.Orders.Remove(order);
            db.SaveChanges();
            return Json(Ok($"Order with Id: {Id} successfully deleted."));
        }
    }
}
