﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dll.Entities;

namespace Booking.Models
{
    public class RoomEquipmentViewModels
    {
        public Room Room { get; set; }
        public List<Equipment> Equipments { get; set; }
        public List<int> SelectedIds { get; set; }
        public List<Department> Departments { get; set; }
    }
}