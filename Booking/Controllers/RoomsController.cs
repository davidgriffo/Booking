using System;
using System.Collections;
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
using Booking = Dll.Entities.Booking;

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

            return View(new RoomBookingViewModel {Room = room, Bookings = bookingsInRoom});
        }

        public JsonResult GetBookings(int id) {
            var bookings = _roomsGateway.GetBookingsForRoom(id);
            var eventList = from booking in bookings
                            select new {
                                id = booking.Id,
                                title = booking.Creator.FirstName + " " + booking.Creator.LastName + "\n" + booking.Room.Name,
                                description = booking.Room.Name,
                                start = booking.FromDate.AddHours(1),
                                end = booking.ToDate.AddHours(1),
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