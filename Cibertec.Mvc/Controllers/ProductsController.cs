using Cibertec.Models;
using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.Mvc.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController : BaseController
    {
        public ProductsController(ILog log, IUnitOfWork unit) : base(log, unit) { }

        // GET: Customer
        public ActionResult Index()
        {
            return View(_unit.Product.GetList());
        }

        //Create
        public PartialViewResult Create()
        {
            return PartialView("_Create", new Products());
        }

        [HttpPost]
        public ActionResult Create(Products product)
        {
            if (ModelState.IsValid)
            {
                _unit.Product.CreateCustomized(product);
                return RedirectToAction("Index");
            }
            product.Discontinued = false;
            product.ProductName = "";
            return PartialView("_Create", product);
        }

        //Edit
        public PartialViewResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_Edit", _unit.Product.GetByIdCustomized(id));
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult Edit(Products product)
        {
            if (ModelState.IsValid)
            {
                _unit.Product.UpdateCustomized(product);
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", product);
        }

        public PartialViewResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_Delete", _unit.Product.GetByIdCustomized(id));
            }

            return PartialView();
        }

        /*Customers customer*/
        [HttpPost]
        [ActionName("Delete")]
        //public ActionResult DeletePost(string CustomerID)
        public ActionResult DeletePost(Products product)
        {
            //_unit.Customers.DeleteCustomized(customer);
            _unit.Product.DeleteCustomized(product.ProductID);
            return RedirectToAction("Index");
        }

        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Products>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Product.PagedList(startRecord, endRecord));
        }

        [Route("Count/{rows:int}")]
        public int Count(int rows)
        {
            var totalRecords = _unit.Product.Count();
            return totalRecords % rows != 0 ? (totalRecords / rows) + 1 : totalRecords / rows;
        }
    }
}