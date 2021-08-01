using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOS
{
    public class PaymentDto
    {
        public string CardNumber { get; set; }
        public long Month { get; set; }
        public long Year { get; set; }
        public string CVC { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }

    }
}
