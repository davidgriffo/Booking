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
        private readonly IGateway<Room, int> _roomGateway = new DllFacade().GetRoomGateway();
        private readonly IGateway<Equipment, int> _equipmentGateway = new DllFacade().GetEquipmentGateway();
        private readonly IGateway<Department, int> _departmentGateway = new DllFacade().GetDepartmentGateway();


        public ActionResult Index() {
            var viewModel = new IndexViewModel {Rooms = _roomGateway.Read(), Equipment = _equipmentGateway.Read(), Departments = _departmentGateway.Read()};
            return View(viewModel);
        }
    }
}