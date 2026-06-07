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
            try
            {
                var product = _productDAL.GetProductByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available in ID " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();

            }
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost, ActionName("Create")]
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
            var products = _productDAL.GetProductByID(id).FirstOrDefault();
            if (products == null)
            {
                TempData["InfoMessage"] = "Product not available in ID " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: ProductController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.UpdateProduct(product);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details Update Successfully....!";
                    }

                    else
                    {
                        TempData["ErrorMessage"] = "Product is already available / Unable to update the product detail";
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _productDAL.GetProductByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available in ID " + id.ToString();
                    return RedirectToAction("Index");

                }
                return View(product);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();

            }
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _productDAL.DeleteProduct(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();

            }

        }
        
    }
}
