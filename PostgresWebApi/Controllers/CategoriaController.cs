using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostgresWebApi.Dtos;
using PostgresWebApi.Models;
using PostgresWebApi.Services;
using System.Text.RegularExpressions;

namespace PostgresWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController(ICategoriaService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaResponse>>> GetItems()
        {
            return Ok(await service.GetCategoriasAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponse>> Get(int id)
        {
            CategoriaResponse? categoria = await service.GetCategoriaByIdAsync(id);

            if (categoria == null)
                return NotFound();
            else
                return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaResponse>> Post([FromBody] CreateCategoriaRequest categoria)
        {
            CategoriaResponse result = await service.AddCategoriaAsync(categoria);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateCategoriaRequest categoria)
        {
            bool updated = await service.UpdateCategoriaAsync(id, categoria);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool removed = await service.DeleteCategoriaAsync(id);
            return removed ? NoContent() : NotFound();
        }
    }
}
