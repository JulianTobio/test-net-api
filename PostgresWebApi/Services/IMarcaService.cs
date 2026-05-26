using PostgresWebApi.Dtos;

namespace PostgresWebApi.Services
{
    public interface IMarcaService
    {
        Task<IList<MarcaResponse>> GetMarcasAsync();
        Task<MarcaResponse?> GetMarcaByIdAsync(int id);
        Task<MarcaResponse> AddMarcaAsync(CreateMarcaRequest marca);
        Task<bool> UpdateMarcaAsync(int id, UpdateMarcaRequest marca);
        Task<bool> DeleteMarcaAsync(int id);
    }
}
