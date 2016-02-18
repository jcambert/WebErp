using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Models
{
    public class UserRole : ModelBase
    {
       
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
