using Microsoft.AspNetCore.Mvc;
using ADO_Examples.Data;
using ADO_Examples.Models;

namespace ADO_Examples.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL _productDAL = new Product_DAL();

       
        // INDEX
      
        public ActionResult Index()
        {
            var list = _productDAL.GetProducts();

            if (list.Count == 0)
                TempData["InfoMessage"] = "No products found";

            return View(list);
        }

       
        // DETAILS
        
        public ActionResult Details(int id)
        {
            var product = _productDAL.GetProductByID(id).FirstOrDefault();

            if (product == null)
            {
                TempData["InfoMessage"] = "Product not found";
                return RedirectToAction("Index");
            }

            return View(product);
        }

       
        // CREATE
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool result = _productDAL.ExecuteProduct(product, "INSERT");

            if (result)
                TempData["SuccessMessage"] = "Inserted successfully";
            else
                TempData["ErrorMessage"] = "Insert failed or duplicate";

            return RedirectToAction("Index");
        }

        // EDIT
       
        public ActionResult Edit(int id)
        {
            var product = _productDAL.GetProductByID(id).FirstOrDefault();

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            bool result = _productDAL.ExecuteProduct(product, "UPDATE");

            if (result)
                TempData["SuccessMessage"] = "Updated successfully";
            else
                TempData["ErrorMessage"] = "Update failed";

            return RedirectToAction("Index");
        }

        
        // DELETE (GET)
        
        public ActionResult Delete(int id)
        {
            var product = _productDAL.GetProductByID(id).FirstOrDefault();

            return View(product);
        }

        
        // DELETE (POST)
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ProductID)
        {
            System.Diagnostics.Debug.WriteLine("DELETE ID = " + ProductID);

            Product p = new Product
            {
                ProductID = ProductID
            };

            bool result = _productDAL.ExecuteProduct(p, "DELETE");

            if (result)
                TempData["SuccessMessage"] = "Deleted successfully!";
            else
                TempData["ErrorMessage"] = "Delete failed!";

            return RedirectToAction("Index");
        }
    }
}