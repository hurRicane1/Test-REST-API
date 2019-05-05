using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebitelTest.Models;
using Microsoft.EntityFrameworkCore;
using WebitelTest.Data;
using WebitelTest.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebitelTest.Controllers
{
    [Route("api/[controller]")]
    public class OrderProductsController : Controller
    {
        private readonly DataContext db;
        public OrderProductsController(DataContext context)
        {
            db = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            var orderProducts = db.OrderProduct.ToList();

            if (orderProducts.Count <= 0)
            {
                return Json(NotFound(Helper.noOrderProducts));
            }
            return Json(Ok(orderProducts));
        }

        // POST api/<controller>
        [HttpPost]
        public JsonResult Post([FromBody]OrderProduct orderProduct)
        {
            if (orderProduct == null)
            {
                return Json(Conflict(Helper.wrongOrderProduct));
            }
            orderProduct.Id = Guid.NewGuid();
            db.OrderProduct.Add(orderProduct);
            
            db.SaveChanges();
            return Json(Ok(orderProduct));
        }
        // DELETE api/<controller>/5
        [HttpDelete("{Id}")]
        public JsonResult Delete(Guid Id)
        {
            var orderProduct = db.OrderProduct.Where(op => op.Id == Id).FirstOrDefault();
            if (orderProduct == null)
            {
                return Json(NotFound(Helper.orderProductNotFound));
            }
            db.OrderProduct.Remove(orderProduct);
            db.SaveChanges();
            return Json(Ok($"Order with Id: {Id} successfully deleted."));
        }
    }
}
