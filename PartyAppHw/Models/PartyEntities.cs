using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PartyAppHw.Models
{
    public partial class PartyEntities : DbContext
    {
        public PartyEntities()
            : base("name=PartyEntities")
        {
        }

        public virtual DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
