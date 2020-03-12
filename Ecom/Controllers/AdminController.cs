using Ecom.EntityF;
using Ecom.Models;
using Ecom.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecom.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _genericUnitOfWork = new GenericUnitOfWork();
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _genericUnitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }
        public ActionResult UpdateCategory(int categoryId)
        {
            DetailsCategory dcategory;
           // Convert.ToString(categoryId);
            if (categoryId != null)
            {
                dcategory = JsonConvert.DeserializeObject<DetailsCategory>(JsonConvert.SerializeObject(_genericUnitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                dcategory = new DetailsCategory();
            }
            return View("UpdateCategory", dcategory);
        }
        public ActionResult CategoryEdit(int catId)
        {
            return View(_genericUnitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {
            _genericUnitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }
        public ActionResult ProductsList()
        {
            return View(_genericUnitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }
        public ActionResult EditProduct(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_genericUnitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
        }
        [HttpPost]
        public ActionResult EditProduct(Tbl_Product editproduct, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/img/ProductImg/"), pic);
                file.SaveAs(path);
            }
            editproduct.ProductImage = file != null ? pic : editproduct.ProductImage;
            editproduct.ModifiedDate = DateTime.Now;
           _genericUnitOfWork.GetRepositoryInstance<Tbl_Product>().Update(editproduct);
            return RedirectToAction("ProductsList");
        }
        public ActionResult AddProduct()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Tbl_Product addproduct, HttpPostedFileBase file)
        {
            string picture = null;
            if (file != null)
            {
                picture = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/img/ProductImg/"), picture);
                file.SaveAs(path);
            }
            addproduct.ProductImage = picture;
            addproduct.CreatedDate = DateTime.Now;
            _genericUnitOfWork.GetRepositoryInstance<Tbl_Product>().Add(addproduct);
            return RedirectToAction("ProductsList");
        }

        //To get Category in dropdown list
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var ct = _genericUnitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach (var item in ct)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
    }
}