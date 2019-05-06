using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BusProviderApplication.Controllers
{
    public class HomeController : Controller
    {
       public ProviderServiceReference.ProvidersWebServiceSoapClient soapClient = new ProviderServiceReference.ProvidersWebServiceSoapClient();
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
         

       
        
        public PartialViewResult InsertBusDetail()
        {
             
            

            return PartialView("_AddBus");
        }
        [HttpPost]
        public ActionResult InsertBusDetail(FormCollection formCollection)
        {
            soapClient.InsertBusDetails(formCollection["BusName"],Convert.ToInt32( formCollection["Capacity"]), formCollection["Type"], formCollection["BusNo"]);

            return RedirectToAction("Index");
        }
        public PartialViewResult GetBusDetails()
        {
             

            return PartialView("_GetBusDetails",soapClient.GetBusDetails());
        }

        public PartialViewResult SeatBook()
        {
            

            return PartialView("_SeatLayout");
        }
        public PartialViewResult AddCity()
        {

            return PartialView("_AddCity");
        }
        [HttpPost]
        public ActionResult AddCity(FormCollection formCollection)
        {
            soapClient.AddCityDetails(formCollection["CityName"] ,formCollection["CityState"]);

            return RedirectToAction("Index");
        }
        public  PartialViewResult GetCityDetails()
        {

            return PartialView("_CityList",soapClient.GetCityDetails());
        }
        public PartialViewResult SearchRoute()
        {
            ViewBag.Source = new SelectList(soapClient.GetCityDetails(), "CityId", "CityName");
            ViewBag.Destination = new SelectList(soapClient.GetCityDetails(), "CityId", "CityName");

            return PartialView("_SearchRoute");
        }
        [HttpPost]
        public ActionResult SearchRoute(FormCollection formCollection)
        {
           var result= soapClient.GetRouteDetails(Convert.ToInt32( formCollection["Source"]),Convert.ToInt32( formCollection["Destination"]),Convert.ToDateTime( formCollection["DateOfJourney"]).Date);

            return View(result);
        }
        public  ActionResult AddRouteDetails()
        {
            ViewBag.Source = new SelectList(soapClient.GetCityDetails(), "CityId", "CityName");
            ViewBag.Destination = new SelectList(soapClient.GetCityDetails(), "CityId", "CityName");
            ViewBag.Bus = new SelectList(soapClient.GetBusDetails(), "BusId", "BusName");
            return PartialView("_AddRouteDetails");
        }
        [HttpPost]
        public ActionResult AddRouteDetails(FormCollection collection)
        { 
                 soapClient.AddRouteDetails(Convert.ToInt32(collection["Bus"]),Convert.ToInt32(collection["Source"]),Convert.ToInt32(collection["Destination"]),
               Convert.ToDateTime(collection["dateOfJourney"]),Convert.ToDouble(collection["price"]),collection["ArrivalTime"], collection["DepartureTime"]);

            return View("Index");
        }
        
        public PartialViewResult GetMapView()
        {
            ViewBag.Source = "latur";
            ViewBag.Destination = "Pune";
            return PartialView("_GetMapView" );
        }
        public ActionResult Pay()
        {

            return PartialView("_Payment");
        }
    }
    
}