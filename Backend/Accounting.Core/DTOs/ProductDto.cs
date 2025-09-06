﻿using Accounting.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.DTOs
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }

        public double UnitPrice { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
