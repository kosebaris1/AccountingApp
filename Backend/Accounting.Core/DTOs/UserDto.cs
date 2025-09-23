using Accounting.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.DTOs
{
    public class UserDto: BaseDto
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public string Email { get; set; }
        public DepartmentDto Department { get; set; }
        public int GroupId { get; set; }

        public GroupDto Group { get; set; }

   
    }
}
