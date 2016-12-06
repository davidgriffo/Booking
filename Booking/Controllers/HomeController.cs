using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Authorization;
using Booking.Models;
using Dll;
using Dll.Entities;
using Dll.Gateways;
using Microsoft.Ajax.Utilities;
namespace Booking.Controllers {
    [RequireUser]
    public class HomeController : Controller {
        private IGateway<Room, int> _rg = new DllFacade().GetRoomGateway();
        private IGateway<Equipment, int> _eg = new DllFacade().GetEquipmentGateway();
        private IGateway<Dll.Entities.Booking, int> _bg = new DllFacade().GetBookingGateway();
        private readonly IGateway<Department, int> _departmentGateway = new DllFacade().GetDepartmentGateway();


        public ActionResult Index() {
            var viewModel = new IndexViewModel {Rooms = _rg.Read(), Equipment = _eg.Read(), Departments = _departmentGateway.Read()};
            return View(viewModel);
        }
    }
}