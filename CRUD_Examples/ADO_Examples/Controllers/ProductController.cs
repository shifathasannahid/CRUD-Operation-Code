using Microsoft.AspNetCore.Http;
using ADO_Examples.Data;
using Microsoft.AspNetCore.Mvc;
using ADO_Examples.Models;

namespace ADO_Examples.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL _productDAL = new Product_DAL();
       
        
        // GET: ProductController
        public ActionResult Index()
        {
            var productList = _productDAL.GetProducts();
           if(productList.Count == 0)
            {
                TempData["InfoMessage"] = "Curreently product not available in the Database.";
            }
            
            return View(productList);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Product product)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _productDAL.InsertProduct(product);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved Successfully....!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product is already available / Unable to save the product detail";
                    }


                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();

            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
