using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Authorization;
using Booking.Models;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers
{
    [RequireUser]
    public class HomeController : Controller
    {
        private IGateway<Room, int> _rg = new DllFacade().GetRoomGateway();
        private IGateway<Equipment, int> _eg = new DllFacade().GetEquipmentGateway();

        public ActionResult Index()
        {
            var model = new RoomEquipmentViewModels
            {
                Equipments = _eg.Read(),
                Room = _rg.Read()
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}