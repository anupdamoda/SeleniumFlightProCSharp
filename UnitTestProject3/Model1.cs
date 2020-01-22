namespace UnitTestProject3
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tblStrip> tblStrips { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblStrip>()
                .Property(e => e.FreightOn)
                .HasPrecision(18, 3);

            modelBuilder.Entity<tblStrip>()
                .Property(e => e.FreightOff)
                .HasPrecision(18, 3);

            modelBuilder.Entity<tblStrip>()
                .Property(e => e.FuelOn)
                .HasPrecision(18, 3);

            modelBuilder.Entity<tblStrip>()
                .Property(e => e.FuelOff)
                .HasPrecision(18, 3);
        }
    }
}
