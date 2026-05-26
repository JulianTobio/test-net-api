using PostgresWebApi.Models;

namespace PostgresWebApi.Dtos
{
    public class ProductoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int Stock { get; set; }

        public decimal Precio { get; set; }

        public string Imagen { get; set; } = string.Empty;
        public int MarcaId { get; set; }

        public string MarcaName { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public string CategoriaName { get; set; } = string.Empty;
    }
}
