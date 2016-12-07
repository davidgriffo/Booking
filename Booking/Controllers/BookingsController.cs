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
    [RequireAdmin]
    public class BookingsController : Controller
    {
        private readonly IGateway<Dll.Entities.Booking, int> _bookingGateway = new DllFacade().GetBookingGateway();
        private IAccountGateway _accountGateway = new DllFacade().GetAccountGateway();

        // GET: Bookings
        public ActionResult Index()
        {
            return View(_bookingGateway.Read());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var booking = _bookingGateway.Read(id.Value);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
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
                _bookingGateway.Create(booking);

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
            var booking = _bookingGateway.Read(id.Value);
            
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
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
                _bookingGateway.Update(booking);

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

            var booking = _bookingGateway.Read(id.Value);

            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _bookingGateway.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
