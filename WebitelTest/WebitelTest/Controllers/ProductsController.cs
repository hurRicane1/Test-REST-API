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
    public class ProductsController : Controller
    {
        private readonly DataContext db;
        public ProductsController(DataContext context)
        {
            db = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            var products = db.Products.ToList();

            if (products.Count <= 0)
            {
                return Json(NotFound(Helper.noProducts));
            }
            return Json(Ok(products));
        }

        // POST api/<controller>
        [HttpPost]
        public JsonResult Post([FromBody]Product product)
        {
            if (product==null)
            {
                return Json(Conflict(Helper.wrongProduct));
            }
            db.Products.Add(product);
            db.SaveChanges();
            return Json(Ok(product));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{Id}")]
        public JsonResult Delete(Guid Id)
        {
            Product product = db.Products.Where(p => p.Id == Id).FirstOrDefault();
            if (product == null)
            {
                return Json(NotFound(Helper.productNotFound));
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return Json(Ok($"Product with Id: {Id} successfully deleted."));
        }
    }
}
