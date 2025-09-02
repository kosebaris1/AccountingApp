using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

    }
}
