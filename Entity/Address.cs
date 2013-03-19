using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Address:BaseEntity<int>
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
