using Microsoft.EntityFrameworkCore;
using PostgresWebApi.Data;
using PostgresWebApi.Dtos;
using PostgresWebApi.Models;

namespace PostgresWebApi.Services
{
    public class MarcaService(MarketDbContext context) : IMarcaService
    {
        public async Task<MarcaResponse> AddMarcaAsync(CreateMarcaRequest marca)
        {
            Marca newMarca = new Marca
            {
                Id = 0,
                Nombre = marca.Nombre,
            };

            context.Marcas.Add(newMarca);
            await context.SaveChangesAsync();

            return new MarcaResponse
            {
                Id = newMarca.Id,
                Nombre = newMarca.Nombre
            };
        }

        public async Task<bool> DeleteMarcaAsync(int id)
        {
            Marca? toDelete = await context.Marcas.FindAsync(id);

            if (toDelete == null)
            {
                return false;
            }

            context.Marcas.Remove(toDelete);
            int removed = await context.SaveChangesAsync();
            return removed == 1;
        }

        public async Task<MarcaResponse?> GetMarcaByIdAsync(int id)
        {
            return await context.Marcas
                .Where(m => m.Id == id)
                .Select(m => new MarcaResponse
                {
                    Id = m.Id,
                    Nombre = m.Nombre
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IList<MarcaResponse>> GetMarcasAsync()
        {
            return await context.Marcas.Select(p => new MarcaResponse
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToListAsync();
        }

        public async Task<bool> UpdateMarcaAsync(int id, UpdateMarcaRequest marca)
        {
            Marca? toEdit = await context.Marcas.FindAsync(id);

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
