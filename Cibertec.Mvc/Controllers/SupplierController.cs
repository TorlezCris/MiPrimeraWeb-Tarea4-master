using Cibertec.UnitOfWork;
using Cibertec.Models;
using System.Web.Mvc;
using log4net;
using System.Collections.Generic;

namespace Cibertec.Mvc.Controllers
{
    [RoutePrefix("Supplier")]
    public class SupplierController : BaseController
    {
        public SupplierController(ILog log, IUnitOfWork unit) : base(log, unit)
        {
            //_unit = unit;
        }

        //Simulación de Error
        public ActionResult Error()
        {
            throw new System.Exception("Prueba de Validación de Error - Action Filter");
        }

        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<Suppliers> vista = _unit.Supplier.GetList();
            return View(vista);
        }

        //Create
        public PartialViewResult Create()
        {
            return PartialView("_Create", new Suppliers());
        }

        [HttpPost]
        public ActionResult Create(Suppliers supplier)
        {
            if (ModelState.IsValid)
            {
                _unit.Supplier.CreateCustomized(supplier);
                return RedirectToAction("Index");
            }
            supplier.ContactName = string.Empty;
            supplier.CompanyName = string.Empty;
            return PartialView("_Create", supplier);
        }

        //Edit
        public PartialViewResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_Edit", _unit.Supplier.GetByIdCustomized(id));
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult Edit(Suppliers supplier)
        {
            if (ModelState.IsValid)
            {
                _unit.Supplier.UpdateCustomized(supplier);
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", supplier);
        }

        public PartialViewResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_Delete", _unit.Supplier.GetByIdCustomized(id));
            }

            return PartialView();
        }

        /*Customers customer*/
        [HttpPost]
        [ActionName("Delete")]
        //public ActionResult DeletePost(string CustomerID)
        public ActionResult DeletePost(Suppliers supplier)
        {
            //_unit.Customers.DeleteCustomized(customer);
            _unit.Supplier.DeleteCustomized(supplier.SupplierID);
            return RedirectToAction("Index");
        }

        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Suppliers>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Supplier.PagedList(startRecord, endRecord));
        }
        [Route("Count/{rows:int}")]
        public int Count(int rows)
        {
            var totalRecords = _unit.Supplier.Count();
            return totalRecords % rows != 0 ? (totalRecords / rows) + 1 : totalRecords / rows;
        }
    }
}