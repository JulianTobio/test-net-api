namespace PostgresWebApi.Dtos
{
    public class UpdateMarcaRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
