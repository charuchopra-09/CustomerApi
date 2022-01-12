using System;
using System.Collections.Generic;
using System.Text;

namespace Customer
{
    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(format: "n");
        public string Name { get; set; }
        public string Country { get; set; }
        public long PhoneNumber { get; set; }
    }

    
}
