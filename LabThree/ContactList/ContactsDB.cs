using System.Data.Entity;

namespace ContactList
{
    class ContactsDB : DbContext
    {
        public ContactsDB() : base("DefaultConnection")
        {

        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
