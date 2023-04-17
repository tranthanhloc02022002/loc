﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Webbanhang;
using Webbanhang.Context;
using static Webbanhang.Common;
namespace Webbanhang.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        locEntities objlocEntities = new locEntities();
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var lstBrand = new List<Brand>();
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
                //lấy ds sản phẩm trong bảng product
                lstBrand = objlocEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstBrand = objlocEntities.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang  =4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa len đầu
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
       
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {

                    if (objBrand.Name != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        //ten hinh.png
                        objBrand.Avartar = fileName;
                        //lưu file hình
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    objlocEntities.Brands.Add(objBrand);
                    objlocEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objBrand);
        }


        [HttpGet]
        public ActionResult Details(int Id)
        {
            var objBrand = objlocEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objlocEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBra)
        {
            var objBrand = objlocEntities.Brands.Where(n => n.Id == objBra.Id).FirstOrDefault();
            objlocEntities.Brands.Remove(objBrand);
            objlocEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objlocEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Brand objBrand)
        {
            if (ModelState.IsValid)
            {
                if (objBrand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    //tenhinh
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    //png
                    fileName = fileName + extension;
                    //ten hinh.png
                    objBrand.Avartar = fileName;
                    //lưu file hình
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                objBrand.UpdatedOnUtc = DateTime.Now;
                objlocEntities.Entry(objBrand).State = System.Data.Entity.EntityState.Modified;
                objlocEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objBrand);
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