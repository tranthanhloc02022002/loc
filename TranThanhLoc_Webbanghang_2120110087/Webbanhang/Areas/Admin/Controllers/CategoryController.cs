using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Context;
using static Webbanhang.Common;

using static Webbanhang.Common.ListtoDataTableConverter;

namespace Webbanhang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        locEntities objlocEntities = new locEntities();
        // GET: Admin/Category
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var lstCategory = new List<Category>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //lấy ds sản phẩm trong bảng category
                lstCategory = objlocEntities.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng category
                lstCategory = objlocEntities.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang  =4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa len đầu
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {

                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        //ten hinh.png
                        objCategory.Avartar = fileName;
                        //lưu file hình
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objlocEntities.Categories.Add(objCategory);
                    objlocEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objCategory);
        }


        [HttpGet]
        public ActionResult Details(int Id)
        {
            var objCategory = objlocEntities.Categories.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objlocEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Category objCate)
        {
            var objCategory = objlocEntities.Categories.Where(n => n.Id == objCate.Id).FirstOrDefault();
            objlocEntities.Categories.Remove(objCategory);
            objlocEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objlocEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Category objCategory)
        {
            if (ModelState.IsValid)
            {
                if (objCategory.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    //tenhinh
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    //png
                    fileName = fileName + extension;
                    //ten hinh.png
                    objCategory.Avartar = fileName;
                    //lưu file hình
                    objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                objCategory.UpdatedOnUtc = DateTime.Now;
                objlocEntities.Entry(objCategory).State = System.Data.Entity.EntityState.Modified;
                objlocEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objCategory);
        }
        void LoadData()
        {
            Common objCommon = new Common();

            //lấy dữ liệu danh muc dưới DB
            var lstCat = objlocEntities.Categories.ToList();
            //Convert sang select list dạng value, text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //lấy dữ liệu thương hiệu dưới DB
            var lstBrand = objlocEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //Convert sang select list dạng value, text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //Loại sản phẩm
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //Convert sang select list dạng value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");


        }
    }
}