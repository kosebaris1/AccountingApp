using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Models
{
    public class Payment: BaseEntity
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public double Amount { get; set; }
    }
}
