using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostgresWebApi.Models;

namespace PostgresWebApi.Data.Configuration
{
    public class ProductosConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Descripcion).IsRequired().HasMaxLength(2000);
            builder.Property(p => p.Imagen).IsRequired(false).HasMaxLength(1000);
            builder.Property(p => p.Precio).HasPrecision(10, 2);
            builder.HasOne(p => p.Marca).WithMany().HasForeignKey(p => p.MarcaId);
            builder.HasOne(p => p.Categoria).WithMany().HasForeignKey(p => p.CategoriaId);
        }
    }
}
