using Microsoft.EntityFrameworkCore;
using PostgresWebApi.Data;
using PostgresWebApi.Dtos;
using PostgresWebApi.Models;

namespace PostgresWebApi.Services
{
    public class CategoriaService(MarketDbContext context) : ICategoriaService
    {
        public async Task<CategoriaResponse> AddCategoriaAsync(CreateCategoriaRequest categoria)
        {
            Categoria newCategoria = new Categoria
            {
                Id = 0,
                Nombre = categoria.Nombre,
            };

            context.Categorias.Add(newCategoria);
            await context.SaveChangesAsync();

            return new CategoriaResponse
            {
                Id = newCategoria.Id,
                Nombre = newCategoria.Nombre
            };
        }

        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            Categoria? toDelete = await context.Categorias.FindAsync(id);

            if (toDelete == null)
            {
                return false;
            }

            context.Categorias.Remove(toDelete);
            int removed = await context.SaveChangesAsync();
            return removed == 1;
        }

        public async Task<IList<CategoriaResponse>> GetCategoriasAsync()
        {
            return await context.Categorias.Select(p => new CategoriaResponse
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToListAsync();
        }

        public async Task<CategoriaResponse?> GetCategoriaByIdAsync(int id)
        {
            return await context.Categorias
                .Where(m => m.Id == id)
                .Select(m => new CategoriaResponse
                {
                    Id = m.Id,
                    Nombre = m.Nombre
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCategoriaAsync(int id, UpdateCategoriaRequest marca)
        {
            Categoria? toEdit = await context.Categorias.FindAsync(id);

            if (toEdit == null)
            {
                return false;
            }

            toEdit.Nombre = marca.Nombre;

            int edited = await context.SaveChangesAsync();
            return edited == 1;
        }
    }
}
