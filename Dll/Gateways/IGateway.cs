using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Gateways {
    public interface IGateway <T, Y> {
        T Create(T element);
        T Read(Y id);
        List<T> Read();
        T Update(T element);
        bool Delete(Y id);
    }
}
