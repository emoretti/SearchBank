using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Branch: BaseEntity<int>
    {
        public string Name { get; set; }
        public string StoreHours { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual Address Address { get; set; }
    }
}
