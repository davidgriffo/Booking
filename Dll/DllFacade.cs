using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;
using Dll.Gateways;

namespace Dll {
    public class DllFacade {
        public AbstractRoomGateway GetRoomGateway() {
            return new RoomGateway();
        }
        public AbstractBookingGateway GetBookingGateway() {
            return new BookingGateway();
        }
        public IGateway<Equipment, int> GetEquipmentGateway() {
            return new EquipmentGateway();
        }
        public AbstractUserGateway GetUserGateway() {
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
