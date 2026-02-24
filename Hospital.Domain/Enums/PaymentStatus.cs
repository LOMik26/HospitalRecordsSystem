using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Enums
{
    public enum PaymentStatus
    {
        Unpaid,  // заявка ещё не оплачена
        Paid     // заявка оплачена
    }
}
