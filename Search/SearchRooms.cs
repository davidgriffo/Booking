using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Search {
    public class SearchRooms {
        public List<Room> CheckAvailibility(List<Room> rooms, List<Booking> bookings, DateTime startDate,
            DateTime endDate) {
            List<Room> returnRooms = new List<Room>();

            foreach (var room in rooms) {
                bool isAvailible = true;

                foreach (var booking in bookings) {
                    if (booking.Room.Id != room.Id) {
                        continue;
                    }

                    //CHECK AVAILIBILITY HERE
                    if (booking.ToDate <= endDate && booking.ToDate >= startDate) {
                        isAvailible = false;
                        break;
                    } else if (booking.FromDate <= endDate && booking.FromDate >= startDate) {
                        isAvailible = false;
                        break;
                    } else if (booking.FromDate <= startDate && booking.ToDate >= endDate) {
                        isAvailible = false;
                        break;
                    }
                }
                if (isAvailible) {
                    returnRooms.Add(room);
                }
            }
            return returnRooms;
        }

        public List<Room> CheckCapacity(List<Room> rooms, int capacityRequired) {

            var returnList = new List<Room>();

            foreach (var room in rooms) {
                if (room.Capacity >= capacityRequired) {
                    returnList.Add(room);
                }
            }

            return returnList;
        }

        public List<Room> CheckEquipment(List<Room> rooms, List<Equipment> equipment) {

            var returnList = new List<Room>();

            var equipmentIdList = new List<int>();

            //Tilf'jer id til liste med equipment
            foreach (var eq in equipment) {
                equipmentIdList.Add(eq.Id);
            }

            //Opretter en ny liste til hvert rum det equipment som er i rummet
            foreach (var room in rooms) {

                var roomEquipmentIdList = new List<int>();

                //Tilf'jer id til liste med equiopment
                foreach (var eq in room.Equipment) {
                    roomEquipmentIdList.Add(eq.Id);
                }


                if (!equipmentIdList.Except(roomEquipmentIdList).Any()) {
                    returnList.Add(room);
                }

            }

            return returnList;
        }

    }
}