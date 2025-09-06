using Accounting.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.DTOs
{
    public class SaleDto : BaseDto
    {
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
