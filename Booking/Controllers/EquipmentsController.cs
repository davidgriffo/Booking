using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Booking.Authorization;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers {
    [RequireSuperAdmin]
    public class EquipmentsController : Controller {
        private readonly IGateway<Equipment, int> _equipmentGateway = new DllFacade().GetEquipmentGateway();

        // GET: Equipments
        public ActionResult Index() {
            return View(_equipmentGateway.Read().OrderBy(x => x.Name));
        }

        // GET: Equipments/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Equipment equipment) {
            if (ModelState.IsValid) {
                _equipmentGateway.Create(equipment);

                return RedirectToAction("Index");
            }

            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var equipment = _equipmentGateway.Read(id.Value);

            if (equipment == null) {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Equipment equipment) {
            if (ModelState.IsValid) {
                _equipmentGateway.Update(equipment);

                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var equipment = _equipmentGateway.Read(id.Value);

            if (equipment == null) {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            _equipmentGateway.Delete(id);

            return RedirectToAction("Index");
        }
    }
}