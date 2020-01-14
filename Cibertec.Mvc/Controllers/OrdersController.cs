using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cibertec.Repositories.Dapper.NorthWind;
using Cibertec.UnitOfWork;
using System.Configuration;
using Cibertec.Models;

namespace Cibertec.Mvc.Controllers
{
    [RoutePrefix("Orders")]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _unit;

        public OrdersController()
        {
            _unit = new NorthwindUnitOfWork(
                ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        }

        // GET: Orders
        public ActionResult Index()
        {
            return View(_unit.Orders.GetList());
        }

        public ActionResult OrderDetail(int id)
        {
            return View(_unit.OrderDetails.GetListByOrderId(id));
        }

        public PartialViewResult Update(int id)
        {
            //return View(_unit.Customers.GetById(id));
            return PartialView("_Update", _unit.Orders.GetByIdCustomized(id));
        }

        [HttpPost]
        public ActionResult Update(oRDERS order)
        {
            var val = _unit.Orders.UpdateOrderCustomized(order);

            if (val)
            {
                return RedirectToAction("Index");
            }
            //return View(customer);
            return PartialView("_Update", order);
        }

        //Paginación
        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<oRDERS>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Orders.PagedList(startRecord, endRecord));
        }
        [Route("Count/{rows:int}")]
        public int Count(int rows)
        {
            var totalRecords = _unit.Orders.Count();
            return totalRecords % rows != 0 ? (totalRecords / rows) + 1 : totalRecords / rows;
        }
    }
}