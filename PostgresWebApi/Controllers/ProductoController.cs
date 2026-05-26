using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostgresWebApi.Dtos;
using PostgresWebApi.Services;

namespace PostgresWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController(IProductoService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IList<ProductoResponse>>> GetProductos()
        {
            return Ok(await service.GetProductosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoResponse>> GetProducto(int id)
        {
            ProductoResponse? producto = await service.GetProductoAsync(id);

            if (producto == null)
                return NotFound();
            else
                return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoResponse>> Post([FromBody] CreateProductoRequest producto)
        {
            ProductoResponse result = await service.AddProductoAsync(producto);

            return CreatedAtAction(nameof(GetProducto), new { id = result.Id }, result);
        }
    }
}
