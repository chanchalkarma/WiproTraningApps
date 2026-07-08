using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoAngularCrudApi.Models
{
    public partial class AngCustDBContext : DbContext
    {
        public AngCustDBContext()
        {
        }

        public AngCustDBContext(DbContextOptions<AngCustDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;

    }
}
