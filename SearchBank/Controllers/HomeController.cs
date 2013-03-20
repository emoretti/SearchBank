using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain.Services;

namespace SearchBank.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var banks = this.UnitOfWork.BankRepository.GetAll().ToList();

           var address = GeoLocalizationService.GetAddressCoordenates("Avenida Bandera de los andes", 1000, "Guaymallén","", "Mendoza",
                                                         "Argentina");

           var addresshuddle = GeoLocalizationService.GetAddressCoordenates("Montevideo", 230, "Mendoza", "", "Mendoza",
                                                        "Argentina");

           var distance = GeoLocalizationService.GetDistanceKmHrs(addresshuddle.Latitude, addresshuddle.Longitude,
                                                                   address);


            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
