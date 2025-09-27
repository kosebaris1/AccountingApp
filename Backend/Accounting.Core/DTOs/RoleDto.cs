using Accounting.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.DTOs
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }
        public List<GroupInRole>? GroupInRoles { get; set; }
    }
}
