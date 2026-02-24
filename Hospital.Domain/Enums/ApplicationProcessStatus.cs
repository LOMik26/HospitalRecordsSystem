using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Enums
{
    public enum ApplicationProcessStatus
    {
        Pending,   // ожидает обработки
        Accepted,  // принято
        Rejected   // отклонено
    }

}
