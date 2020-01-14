using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cibertec.Repositories.Dapper.NorthWind;
using Cibertec.UnitOfWork;
using System.Configuration;
using Cibertec.Models;
using log4net;
using Cibertec.Mvc.ActionFilters;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Cibertec.Mvc.Controllers
{
    //[ErrorActionFilter]
    [RoutePrefix("Customer")]
    public class CustomerController : BaseController
    {
        /*Ya no es necesario porque se maneja en el padre BaseController*/
        //private readonly IUnitOfWork _unit;

        /*Ya no es necesario porque se utiliza Inyección de Dependencia*/
        /*
        public CustomerController()
        {
            _unit = new NorthwindUnitOfWork(
                ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        }
        */
        public CustomerController(ILog log, IUnitOfWork unit): base(log,unit)
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
            _log.Info("Ejecución de Customer Controller Ok");
            return View(_unit.Customers.GetList());
        }

        //CREATE: Customer
        //public ActionResult Create()
        public PartialViewResult Create()
        {
            //return View();
            return PartialView("_Create", new Customers());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _unit.Customers.Insert(customer);
                return RedirectToAction("Index");
            }
            //return View(customer);
            return PartialView("_Create", customer);
        }

        //public ActionResult Update(string id)
        public PartialViewResult Update(string id)
        {
            //return View(_unit.Customers.GetById(id));
            return PartialView("_Update",_unit.Customers.GetById(id));
        }

        [HttpPost]
        public ActionResult Update(Customers customer)
        {
            var val = _unit.Customers.Update(customer);

            if (val)
            {
                return RedirectToAction("Index");
            }
            //return View(customer);
            return PartialView("_Update", customer);
        }

        //public ActionResult Delete(String id)
        public PartialViewResult Delete(String id)
        {
            //return View(_unit.Customers.GetById(id));
            return PartialView("_Delete",_unit.Customers.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(String id)
        {
            var val = _unit.Customers.Delete(id);

            if (val) return RedirectToAction("Index");
            //return View();
            return PartialView("_Delete", _unit.Customers.GetById(id));
        }

        [Route("List/{page:int}/{rows:int}")]
        public async Task<PartialViewResult> List(int page, int rows)
        {
            var listOfPage = new List<Customers>();
            if (page <= 0 || rows <= 0) return PartialView(listOfPage);

            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            var url = "http://localhost:55724/";    //Api's url
            var identity = (ClaimsIdentity)User.Identity;

            //Recovering param access token
            var token = identity.Claims.FirstOrDefault(x => x.Type.ToLower().Contains("authentication")).Value;

            //Call Api for get data of a page.
            var data = await GetPages(startRecord, endRecord, url, token);
            if(data.Any() && data.Count() > 0) listOfPage = data.ToList();

            return PartialView("_List", listOfPage);
        }
        [Route("Count/{rows:int}")]
        public int Count(int rows)
        {
            var totalRecords = _unit.Customers.Count();
            return totalRecords % rows != 0 ? (totalRecords / rows) + 1 : totalRecords / rows; 
        }



        public async Task<IEnumerable<Customers>> GetPages(int inicio, int final, string url, string token)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var urlPages = $"{url}/customer/list/{inicio}/{final}";
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpRequest = new HttpRequestMessage(HttpMethod.Get, urlPages);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var r = await client.SendAsync(httpRequest);
                    if (r.StatusCode == System.Net.HttpStatusCode.InternalServerError || r.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return null;
                    }
                    string jsonString = await r.Content.ReadAsStringAsync();
                    IEnumerable<Customers> responseData = JsonConvert.DeserializeObject<IEnumerable<Customers>>(jsonString);
                    return responseData;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}