using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Task2.Core.Model;

namespace Task2.Data.Configurations
{
    public class TextConfiguration : IEntityTypeConfiguration<Text>
    {
        public void Configure(EntityTypeBuilder<Text> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Type);
            builder.Property(t => t.Input);
            builder.ToTable("Texts");
            //da li ovde zpravo definisem kolone tabele
            //ovde se definisu i relatioships ako ih ima sa drugim entitetima

        }
    }
}
