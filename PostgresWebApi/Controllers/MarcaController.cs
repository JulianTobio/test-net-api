using Microsoft.AspNetCore.Mvc;
using PostgresWebApi.Dtos;
using PostgresWebApi.Models;
using PostgresWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostgresWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController(IMarcaService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaResponse>>> GetItems()
        {
            return Ok(await service.GetMarcasAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaResponse>> Get(int id)
        {
            MarcaResponse? marca = await service.GetMarcaByIdAsync(id);

            if (marca == null)
                return NotFound();
            else
                return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult<MarcaResponse>> Post([FromBody] CreateMarcaRequest marca)
        {
            MarcaResponse result = await service.AddMarcaAsync(marca);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateMarcaRequest marca)
        {
            bool updated = await service.UpdateMarcaAsync(id, marca);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool removed = await service.DeleteMarcaAsync(id);
            return removed ? NoContent() : NotFound();
        }
    }
}
