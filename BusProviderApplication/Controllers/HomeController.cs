using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusProviderApplication.Controllers
{
    public class HomeController : Controller
    {
        ProviderServiceReference.ProvidersWebServiceSoapClient soapClient = new ProviderServiceReference.ProvidersWebServiceSoapClient();
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
        public PartialViewResult GetProviders()
        {
            var result = soapClient.GetProviderDetails();
            
            return PartialView("_ProviderList",result);
        }

        public PartialViewResult InsertProviderDetail()
        {
            //var result = soapClient.InsertIntoProvider();

            return PartialView("_InsertProvider");
        }
        [HttpPost]
        public ActionResult InsertProviderDetail(FormCollection formCollection)
        {
            soapClient.InsertIntoProvider(formCollection["OrganisationName"],formCollection["ContactNo"]);

            return RedirectToAction("GetProviders");
        }
        public PartialViewResult InsertBusDetail()
        {
             
            ViewBag.ProviderList = new SelectList(soapClient.GetProviderDetails(), "ProviderID", "OrganisationName");

            return PartialView("_AddBus");
        }
        [HttpPost]
        public ActionResult InsertBusDetail(FormCollection formCollection)
        {
            soapClient.InsertBusDetails(formCollection["BusName"],Convert.ToInt32( formCollection["Capacity"]), formCollection["Type"], formCollection["BusNo"],Convert.ToInt32( formCollection["ProviderList"]));

            return RedirectToAction("GetBusDetails");
        }
        public PartialViewResult GetBusDetails()
        {
             

            return PartialView("_GetBusDetails",soapClient.GetBusDetails());
        }
    }
}