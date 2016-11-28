using System;
using System.Collections.Generic;
using Dll.Entities;
using NUnit.Framework;
using Search;

namespace SearchTest {
    [TestFixture]
    public class SearchTest {
        
        [Test]
        public void TestRoomsAvailibility() {
            // Test if a list of rooms is availible
            // Send in a start date and end time
            // Send back a list of the rooms that are availible

            var r1 = new Room {Id = 1};
            var r2 = new Room {Id = 2};
            var r3 = new Room {Id = 3};

            var roomsList = new List<Room> {r1, r2, r3};

            var b1 = new Booking {Id = 1, Room = r1, FromDate = new DateTime(2016, 11, 27, 9, 0, 0), ToDate = new DateTime(2016, 11, 27, 10, 0, 0)};
            var b2 = new Booking { Id = 2, Room = r2, FromDate = new DateTime(2016, 11, 27, 11, 30, 0), ToDate = new DateTime(2016, 11, 27, 14, 20, 0) };
            var b3 = new Booking { Id = 3, Room = r3, FromDate = new DateTime(2016, 11, 27, 8, 0, 0), ToDate = new DateTime(2016, 11, 27, 9, 40, 0) };


            var bookingsList = new List<Booking> {b1, b2, b3};

            var startDate = new DateTime(2016, 11, 27, 9, 30, 0);
            var endDate = new DateTime(2016, 11, 27, 10, 0, 0);

            // CALL FUNCTION TO INVOKE SEARCH
            var returnList = new SearchRooms().CheckAvailibility(roomsList, bookingsList, startDate, endDate);

            Assert.IsTrue(returnList.Count == 1);
        }

        [Test]
        public void TestRoomsAvailibilityAllScenarios() {
            var r1 = new Room { Id = 1 };
            var r2 = new Room { Id = 2 };
            var r3 = new Room { Id = 3 };
            var r4 = new Room { Id = 4 };

            var roomsList = new List<Room> { r1, r2, r3, r3 };

            var b1 = new Booking { Id = 1, Room = r1, FromDate = new DateTime(2016, 11, 27, 8, 30, 0), ToDate = new DateTime(2016, 11, 27, 9, 30, 0) };
            var b2 = new Booking { Id = 2, Room = r2, FromDate = new DateTime(2016, 11, 27, 9, 30, 0), ToDate = new DateTime(2016, 11, 27, 10, 30, 0) };
            var b3 = new Booking { Id = 3, Room = r3, FromDate = new DateTime(2016, 11, 27, 8, 30, 0), ToDate = new DateTime(2016, 11, 27, 10, 30, 0) };
            var b4 = new Booking { Id = 4, Room = r4, FromDate = new DateTime(2016, 11, 27, 9, 15, 0), ToDate = new DateTime(2016, 11, 27, 9, 45, 0) };


            var bookingsList = new List<Booking> { b1, b2, b3, b4 };

            var startDate = new DateTime(2016, 11, 27, 9, 0, 0);
            var endDate = new DateTime(2016, 11, 27, 10, 0, 0);

            // CALL FUNCTION TO INVOKE SEARCH
            var returnList = new SearchRooms().CheckAvailibility(roomsList, bookingsList, startDate, endDate);

            Assert.IsTrue(returnList.Count == 0);
        }

        [Test]
        public void TestRoomCapacity() {
            // Test if a list of rooms is the right capacity
            // Send in the capacity required

            var r1 = new Room { Id = 1, Capacity = 10};
            var r2 = new Room { Id = 2, Capacity = 15};
            var r3 = new Room { Id = 3, Capacity = 20};

            var roomsList = new List<Room> { r1, r2, r3 };

            var capacity = 15;

            //CALL FUNCTION TO INVOKE SEARCH
            var returnList = new SearchRooms().CheckCapacity(roomsList, capacity);

            Assert.IsTrue(returnList.Count == 2);
        }

        [Test]
        public void TestRoomEquipment() {

            var e1 = new Equipment {Id = 1, Name = "Projektor"};
            var e2 = new Equipment {Id = 2, Name = "Whiteboard"};

            var r1 = new Room { Id = 1, Equipment = new List<Equipment> {e1, e2} };
            var r2 = new Room { Id = 2, Equipment =  new List<Equipment> {e1} };
            var r3 = new Room { Id = 3, Equipment = new List<Equipment> {e2} };
            var r4 = new Room { Id = 4, Equipment = new List<Equipment>() };

            var roomsList = new List<Room> { r1, r2, r3, r4 };


            var equipment = new List<Equipment> {e1, e2};

            //CALL FUNCTION TO INVOKE SEARCH
            var returnList = new SearchRooms().CheckEquipment(roomsList, equipment);

            Assert.IsTrue(returnList.Count == 1);
        }


    }
}