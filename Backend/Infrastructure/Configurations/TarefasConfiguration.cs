using Entities.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class TarefasConfiguration : IEntityTypeConfiguration<Tarefas>
    {
        public void Configure(EntityTypeBuilder<Tarefas> builder)
        {
            builder.ToTable("Tarefas");

            builder.HasKey(u => u.id);
            builder.Property(u => u.id).ValueGeneratedOnAdd();

            builder.Property(t => t.titulo).IsRequired();
            builder.Property(t => t.descricao).IsRequired();

            builder.Property(t => t.concluida)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property<int>("usuarioId");

            builder.HasOne<Usuarios>()
                   .WithMany()
                   .HasForeignKey("usuarioId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

        }

    }
}