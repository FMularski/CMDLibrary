using CMDLibrary.EntitiesConfigurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLibrary
{
    class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public LibraryContext() : base("name=Library") { ; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
