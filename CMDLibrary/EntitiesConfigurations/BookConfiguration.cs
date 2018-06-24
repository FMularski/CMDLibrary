﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLibrary.EntitiesConfigurations
{
    class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            HasKey(b => b.ISBN);

            Property(b => b.ISBN)
                .HasMaxLength(13 * 2);

            Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(128);
        }
    }
}
