﻿using System;
using System.Collections.Generic;
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
    [RequireUser]
    public class ProfileController : Controller {
        private readonly IAccountGateway _accountGateway = new DllFacade().GetAccountGateway();
        private readonly AbstractUserGateway _userGateway = new DllFacade().GetUserGateway();
        private readonly AbstractBookingGateway _bookingGateway = new DllFacade().GetBookingGateway();

        // GET: Profile
        public ActionResult Index() {
            return View(_accountGateway.GetUserLoggedIn());
        }

        public ActionResult ChangePassword() {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model) {
            if (ModelState.IsValid) {
                var succes = _accountGateway.ChangePassword(model.OldPassword, model.NewPassword);

                if (succes) {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Error updating user");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit() {
            return View(_accountGateway.GetUserLoggedIn());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id, FirstName, LastName, Email, PhoneNumber, isAdmin, isSuperAdmin")] User user) {
            if (ModelState.IsValid) {
                var updatedUser = _userGateway.Update(user);

                if (updatedUser != null) {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        public ActionResult Bookings() {
            var bookings = _userGateway.GetBookingsForUser();
            return View(bookings);
        }

        public JsonResult GetBookings() {
            var bookings = _userGateway.GetBookingsForUser();

            var bookingsList = from e in bookings
                select
                new {
                    id = e.Id,
                    title = e.Creator.FirstName + " " + e.Creator.LastName + "\n" + e.Room.Name,
                    start = e.FromDate.AddHours(1),
                    end = e.ToDate.AddHours(1),
                    allDay = false
                };
            var bookingRows = bookingsList.ToArray();


            return Json(bookingRows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvites() {
            var bookings = _userGateway.GetInvitesForUser();

            var bookingsList = from e in bookings
                               select
                               new {
                                   id = e.Id,
                                   title = e.Creator.FirstName + " " + e.Creator.LastName + "\n" + e.Room.Name,
                                   start = e.FromDate.AddHours(1),
                                   end = e.ToDate.AddHours(1),
                                   allDay = false
                               };
            var bookingRows = bookingsList.ToArray();


            return Json(bookingRows, JsonRequestBehavior.AllowGet);
        }

        // GET: Bookings/Edit/5
        public ActionResult EditBooking(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var booking = _bookingGateway.Read(id.Value);

            if (booking == null) {
                return HttpNotFound();
            }
            if (_accountGateway.GetUserLoggedIn().IsSuperAdmin) {
                return View(booking);
            } else if (_accountGateway.GetUserLoggedIn().Id == booking.Creator.Id) {
                return View(booking);
            }
            return RedirectToAction("Index");
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBooking([Bind(Include = "Id,FromDate,ToDate")] Dll.Entities.Booking booking) {
            if (ModelState.IsValid) {
                _bookingGateway.Update(booking);

                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Details/5
        public ActionResult BookingDetails(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new UsersBookingViewModel {
                Users = _userGateway.Read(),
                Booking = _bookingGateway.Read(id.Value)
            };


/**/
            var booking = _bookingGateway.Read(id.Value);

            if (booking == null) {
                return HttpNotFound();
            }
            if (_accountGateway.GetUserLoggedIn().IsSuperAdmin) {
                return View(model);
            } else if (_accountGateway.GetUserLoggedIn().Id == booking.Creator.Id) {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        // GET: Bookings/Delete/5
        public ActionResult DeleteBooking(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var booking = _bookingGateway.Read(id.Value);

            if (booking == null) {
                return HttpNotFound();
            }
            if (_accountGateway.GetUserLoggedIn().IsSuperAdmin) {
                return View(booking);
            } else if (_accountGateway.GetUserLoggedIn().Id == booking.Creator.Id) {
                return View(booking);
            }
            return RedirectToAction("Index");
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("DeleteBooking")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            _bookingGateway.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult InviteUsers(List<String> users, int bookingId) {
            var selectedUsers = new List<User>();
            users?.ForEach(x => selectedUsers.Add(new User {Id = x}));

            var booking = _bookingGateway.Read(bookingId);
            _bookingGateway.InviteUsers(booking, selectedUsers);

            return RedirectToAction("BookingDetails", new {Id = booking.Id});
        }
    }
}