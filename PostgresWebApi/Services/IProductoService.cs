using PostgresWebApi.Dtos;

namespace PostgresWebApi.Services
{
    public interface IProductoService
    {
        Task<IList<ProductoResponse>> GetProductosAsync();
        Task<ProductoResponse?> GetProductoAsync(int id);
        Task<ProductoResponse> AddProductoAsync(CreateProductoRequest producto);
        Task<bool> UpdateProductoAsync(int id,  UpdateProductoRequest producto);
        Task<bool> DeleteProductoAsync(int id);
    }
}
