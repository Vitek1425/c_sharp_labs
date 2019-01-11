using System.Data.Entity;

namespace FuelStantion
{
    public class FuelStorageDB : DbContext
    {
        public FuelStorageDB() : base("DefaultConnection")
        {
        }
        public DbSet<FuelStorage> FuelStorages { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<FuelRefilling> FuelRefillings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
