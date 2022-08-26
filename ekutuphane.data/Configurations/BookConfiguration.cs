using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;
using Microsoft.EntityFrameworkCore;

namespace ekutuphane.data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b=>b.BookId);
            builder.Property(b=>b.BookName).IsRequired().HasMaxLength(70);
        }
    }
}