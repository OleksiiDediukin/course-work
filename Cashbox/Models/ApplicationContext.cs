using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Cashbox.Models
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Supply> Supplies { get; set; }

        public DbSet<Consignment> Consignments { get; set; }

        public DbSet<Inventory> Inventories { get; set; }
    }
}
