using PostgresWebApi.Dtos;

namespace PostgresWebApi.Services
{
    public interface ICategoriaService
    {
        Task<IList<CategoriaResponse>> GetCategoriasAsync();
        Task<CategoriaResponse?> GetCategoriaByIdAsync(int id);
        Task<CategoriaResponse> AddCategoriaAsync(CreateCategoriaRequest categoria);
        Task<bool> UpdateCategoriaAsync(int id, UpdateCategoriaRequest categoria);
        Task<bool> DeleteCategoriaAsync(int id);
    }
}
