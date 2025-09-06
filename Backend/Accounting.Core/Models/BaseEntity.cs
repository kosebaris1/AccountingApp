using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int Createdby { get; set; }
        public int UpdatedBy { get; set; }

        public bool Status { get; set; }
    }
}
