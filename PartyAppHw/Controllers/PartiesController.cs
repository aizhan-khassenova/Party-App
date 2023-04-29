using PartyAppHw.Models;
using PartyAppHw.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PartyAppHw.Controllers
{
    public class PartiesController : Controller
    {
        private readonly DeliveryInformationRequestsService notificationService;

        public PartiesController(DeliveryInformationRequestsService notificationService)
        {
            this.notificationService = notificationService;
        }

        // GET: Parties
        public ActionResult Index()
        {
            return View(notificationService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(DeliveryRequest model)
        {
            if (ModelState.IsValid)
            {
                notificationService.Save(model);
                notificationService.Notify(model);
                //Выполним отправку уведомления
                return View("RegisterConfirmed", model);
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            notificationService.Delete(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                notificationService.Dispose();
            }
        }

        
    }
}