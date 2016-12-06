using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Booking.Authorization;
using Booking.Models;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers {
    [RequireAdmin]
    public class RoomsController : Controller {
        private readonly AbstractRoomGateway _roomsGateway = new DllFacade().GetRoomGateway();
        private readonly IGateway<Equipment, int> _equipmentGateway = new DllFacade().GetEquipmentGateway();
        private readonly IGateway<Department, int> _departmentsGateway = new DllFacade().GetDepartmentGateway();

        // GET: Rooms
        public ActionResult Index() {
            return View(_roomsGateway.Read());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = _roomsGateway.Read(id.Value);
            if (room == null) {
                return HttpNotFound();
            }
            var bookingsInRoom = _roomsGateway.GetBookingsForRoom(room.Id);
            var events = "";
            foreach (var booking in bookingsInRoom) {
                events += "{";
                events += $"title: '{booking.Creator.FirstName} {booking.Creator.LastName}',start: '{booking.FromDate}, end: '{booking.ToDate}'";
                events += "},";
            }

            return View(new RoomBookingViewModel {Room = room, Events = events, Bookings = bookingsInRoom});
        }

        public JsonResult GetBookings(int id) {
            var ApptListForDate = _roomsGateway.GetBookingsForRoom(id);
            var eventList = from e in ApptListForDate
                            select new {
                                id = e.Id,
                                title = e.Creator.FirstName + " " + e.Creator.LastName,
                                start = e.FromDate,
                                end = e.ToDate,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        // GET: Rooms/Create
        public ActionResult Create() {
            var model = new RoomEquipmentViewModels {Equipments = _equipmentGateway.Read(), Departments = _departmentsGateway.Read()};

            return View(model);
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Department,Equipment,Capacity")] Room room,
            List<int> equipment) {
            if (ModelState.IsValid) {
                room.Equipment = new List<Equipment>();
                equipment?.ForEach(x => room.Equipment.Add(new Equipment {Id = x}));
                _roomsGateway.Create(room);

                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = _roomsGateway.Read(id.Value);
            var equipment = _equipmentGateway.Read();

            if (room == null) {
                return HttpNotFound();
            }

            var tempList = new List<int>();
            room.Equipment.ForEach(x => tempList.Add(x.Id));

            return View(new RoomEquipmentViewModels {Equipments = equipment, Room = room, SelectedIds = tempList, Departments = _departmentsGateway.Read()});
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Department,Capacity")] Room room,
            List<int> equipment) {
            if (ModelState.IsValid) {
                room.Equipment = new List<Equipment>();
                equipment?.ForEach(x => room.Equipment.Add(new Equipment {Id = x}));
                _roomsGateway.Update(room);

                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = _roomsGateway.Read(id.Value);

            if (room == null) {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            var room = _roomsGateway.Delete(id);

            return RedirectToAction("Index");
        }
    }
}