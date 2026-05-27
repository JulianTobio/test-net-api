using Microsoft.EntityFrameworkCore;
using PostgresWebApi.Data;
using PostgresWebApi.Dtos;
using PostgresWebApi.Models;

namespace PostgresWebApi.Services
{
    public class ProductoService(MarketDbContext context) : IProductoService
    {
        public async Task<ProductoResponse> AddProductoAsync(CreateProductoRequest producto)
        {
            Producto newProducto = new Producto
            {
                Id = 0,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Imagen = producto.Imagen,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                MarcaId = producto.MarcaId
            };

            context.Productos.Add(newProducto);
            await context.SaveChangesAsync();

            return new ProductoResponse
            {
                Id = newProducto.Id,
                Nombre = newProducto.Nombre,
                Descripcion = newProducto.Descripcion,
                Precio = newProducto.Precio,
                Imagen = newProducto.Imagen,
                Stock = newProducto.Stock,
                CategoriaId = newProducto.CategoriaId,
                CategoriaName = string.Empty,
                MarcaId = newProducto.MarcaId,
                MarcaName = string.Empty
            };
        }

        public Task<bool> DeleteProductoAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductoResponse?> GetProductoAsync(int id)
        {
            return await context.Productos
                .Where(p => p.Id == id)
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Imagen = p.Imagen,
                    CategoriaId = p.CategoriaId,
                    CategoriaName = p.Categoria != null ? p.Categoria.Nombre : string.Empty,
                    MarcaId = p.MarcaId,
                    MarcaName = p.Marca != null ? p.Marca.Nombre : string.Empty,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IList<ProductoResponse>> GetProductosAsync()
        {
            return await context.Productos.Select(p => new ProductoResponse
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                Imagen = p.Imagen,
                CategoriaId = p.CategoriaId,
                CategoriaName = p.Categoria != null ? p.Categoria.Nombre : string.Empty,
                MarcaId = p.MarcaId,
                MarcaName = p.Marca != null ? p.Marca.Nombre : string.Empty,
            }).ToListAsync();
        }

        public Task<bool> UpdateProductoAsync(int Id, UpdateProductoRequest producto)
        {
            throw new NotImplementedException();
        }
    }
}
