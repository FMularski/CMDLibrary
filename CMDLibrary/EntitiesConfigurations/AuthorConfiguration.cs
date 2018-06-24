using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLibrary.EntitiesConfigurations
{
    class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(64);

            Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}
