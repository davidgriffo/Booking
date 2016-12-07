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
    public class DepartmentsController : Controller {
        private readonly IGateway<Department, int> _departmentGateway = new DllFacade().GetDepartmentGateway();

        // GET: Departments
        public ActionResult Index() {
            return View(_departmentGateway.Read().OrderBy(x => x.Name));
        }

        // GET: Departments/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Department department) {
            if (ModelState.IsValid) {
                _departmentGateway.Create(department);

                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _departmentGateway.Read(id.Value);

            if (department == null) {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Department equipment) {
            if (ModelState.IsValid) {
                _departmentGateway.Update(equipment);

                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _departmentGateway.Read(id.Value);

            if (department == null) {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            _departmentGateway.Delete(id);

            return RedirectToAction("Index");
        }
    }
}