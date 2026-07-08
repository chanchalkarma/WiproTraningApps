using System;
using System.Collections.Generic;

namespace DemoAngularCrudApi.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public int? Amt { get; set; }
    }
}
