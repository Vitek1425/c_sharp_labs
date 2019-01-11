using System.Data.Entity;

namespace FuelStantion
{
    class DatabaseConnection : DbContext
    {
        public DatabaseConnection() : base("DefaultConnection")
        {

        }
    }
}
