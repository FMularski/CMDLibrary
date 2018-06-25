using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLibrary.EntitiesConfigurations
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(32);

            Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(32);

            Property(u => u.Role)
                .IsRequired();
        }
    }
}
