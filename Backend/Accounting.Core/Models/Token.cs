using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Models
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expriration { get; set; }
        public string RefleshToken { get; set; }
    }
}
