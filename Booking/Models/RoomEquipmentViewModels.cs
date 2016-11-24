using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dll.Entities;

namespace Booking.Models
{
    public class RoomEquipmentViewModels
    {
        public List<Room> Room { get; set; }
        public List<Equipment> Equipments { get; set; }
    }
}