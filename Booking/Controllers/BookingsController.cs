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


namespace Booking.Controllers
{
    [RequireUser]
    public class BookingsController : Controller
    {
        private IGateway<Dll.Entities.Booking, int> _bm = new DllFacade().GetBookingGateway();
        private IAccountGateway _ag = new DllFacade().GetAccountGateway();

        // GET: Bookings
        public ActionResult Index()
        {
            if (_ag.GetUserLoggedIn().IsSuperAdmin)
            {
                return View(_bm.Read());
            }

            List<Dll.Entities.Booking> bookings = new List<Dll.Entities.Booking>();

            foreach (var booking in _bm.Read()) {
                if (booking.Creator.Id == _ag.GetUserLoggedIn().Id) {
                    bookings.Add(booking);
                }
            }

            return View(bookings);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var booking = _bm.Read(id.Value);

            if (booking == null)
            {
                return HttpNotFound();
            }
            if (_ag.GetUserLoggedIn().IsSuperAdmin) {
                return View(booking);
            } else if (_ag.GetUserLoggedIn().Id == booking.Creator.Id) {
                return View(booking);
            }
            return RedirectToAction("Index");
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FromDate,ToDate")] Dll.Entities.Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bm.Create(booking);

                return RedirectToAction("Index");
            }

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var booking = _bm.Read(id.Value);
            
            if (booking == null)
            {
                return HttpNotFound();
            }
            if (_ag.GetUserLoggedIn().IsSuperAdmin) {
                return View(booking);
            } else if (_ag.GetUserLoggedIn().Id == booking.Creator.Id) {
                return View(booking);
            }
            return RedirectToAction("Index");
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromDate,ToDate")] Dll.Entities.Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bm.Update(booking);

                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var booking = _bm.Read(id.Value);

            if (booking == null)
            {
                return HttpNotFound();
            }
            if (_ag.GetUserLoggedIn().IsSuperAdmin) {
                return View(booking);
            } else if (_ag.GetUserLoggedIn().Id == booking.Creator.Id)
            {
                return View(booking);
            }
            return RedirectToAction("Index");
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _bm.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
