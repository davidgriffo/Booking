using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;
using Dll.Gateways;

namespace Dll {
    public class DllFacade {
        public IGateway<Room, int> GetRoomGateway() {
            return new RoomGateway();
        }
        public IGateway<Booking, int> GetBookingGateway() {
            return new BookingGateway();
        }
        public IGateway<Equipment, int> GetEquipmentGateway() {
            return new EquipmentGateway();
        }
        public IGateway<User, string> GetUserGateway() {
            return new UserGateway();
        }
        public IAccountGateway GetAccountGateway() {
            return new AccountGateway();
        }
        public IGateway<Department, int> GetDepartmentGateway() {
            return new DepartmentGateway();
        }
    }
}
