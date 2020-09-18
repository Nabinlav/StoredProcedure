using StoredProcedure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoredProcedure.Controllers
{
    public class HomeController : Controller
    {
        StudentDBEntities db = new StudentDBEntities();
        public ActionResult Index()
        {
            var lst = db.ListAllProduct().ToList();
            return View(lst); ;
            
        }

        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblProduct collection)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@productname", collection.ProductName);
                SqlParameter param2 = new SqlParameter("@categoryid", collection.CategoryId);
                SqlParameter param3 = new SqlParameter("@price", collection.Price);
                SqlParameter param4 = new SqlParameter("@quantity", collection.Quantity);
                var data = db.Database.ExecuteSqlCommand("AddProduct @productname,@categoryid,@price,@quantity", param1, param2, param3, param4);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                return View();
            }           
        }

        public ActionResult Details(int id)
        {
            SqlParameter param1 = new SqlParameter("@productid", id);
            var data = db.Database.SqlQuery<tblProduct>("exec ListProductByID @productid",param1).FirstOrDefault();
            return View(data);
        }
        public ActionResult Update(int id)
        {
            SqlParameter param1 = new SqlParameter("@productid", id);
            var data = db.Database.SqlQuery<tblProduct>("exec ListProductByID @productid", param1).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Update(tblProduct tb)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@productname", tb.ProductName);
                SqlParameter param2 = new SqlParameter("@categoryid", tb.CategoryId);
                SqlParameter param3 = new SqlParameter("@price", tb.Price);
                SqlParameter param4 = new SqlParameter("@quantity", tb.Quantity);
                SqlParameter param5 = new SqlParameter("@productid", tb.ProductId);
                var data = db.Database.ExecuteSqlCommand("UpdateProduct @productid, @productname,@categoryid,@price,@quantity",param5, param1, param2, param3, param4);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            SqlParameter param1 = new SqlParameter("@productid", id);
            var data = db.Database.SqlQuery<tblProduct>("exec ListProductByID @productid", param1).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id, tblProduct tb)
        {
            try
            {
                SqlParameter param = new SqlParameter("@productid", id);
                db.Database.ExecuteSqlCommand("DeleteProduct @productid", param);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
           
            
        }
    }
}