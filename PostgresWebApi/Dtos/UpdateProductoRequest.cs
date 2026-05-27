namespace PostgresWebApi.Dtos
{
    public class UpdateProductoRequest
    {
        public required string Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int Stock { get; set; }

        public decimal Precio { get; set; }

        public string Imagen { get; set; } = string.Empty;
        public int MarcaId { get; set; }
        public int CategoriaId { get; set; }
    }
}
