using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresWebApi.Controllers;
using PostgresWebApi.Data;
using PostgresWebApi.Dtos;
using PostgresWebApi.Models;
using PostgresWebApi.Services;

namespace PostgresWebApi.Test
{
    public class MarcaControllerTest
    {
        private async Task<MarketDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            MarketDbContext dbContext = new MarketDbContext(options);
            await dbContext.Database.EnsureCreatedAsync();

            if (await dbContext.Marcas.CountAsync() < 1)
            {
                dbContext.Marcas.Add(new Marca() { Nombre = "Adidas" });
                dbContext.Marcas.Add(new Marca() { Nombre = "Nike" });
                dbContext.Marcas.Add(new Marca() { Nombre = "Umbro" });

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }
        
        [Fact]
        public async Task MarcaController_GetItems_ReturnMarcaResponseArray()
        {
            // Arrange
            MarketDbContext dbContext = await GetDbContext();
            MarcaService service = new MarcaService(dbContext);
            MarcaController controller = new MarcaController(service);

            // Act
            ActionResult<IEnumerable<MarcaResponse>> result = await controller.GetItems();

            // Asert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            IEnumerable<MarcaResponse> marcas = Assert.IsType<IEnumerable<MarcaResponse>>(objectResult.Value, exactMatch: false);
            Assert.NotEmpty(marcas);
            Assert.Equal(3, marcas.Count());
        }

        [Fact]
        public async Task MarcaController_GetItem_ReturnMarcaResponse()
        {
            // Arrange
            MarketDbContext dbContext = await GetDbContext();
            MarcaService service = new MarcaService(dbContext);
            MarcaController controller = new MarcaController(service);

            // Act
            ActionResult<MarcaResponse> result = await controller.Get(1);

            // Asert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            MarcaResponse marca = Assert.IsType<MarcaResponse>(objectResult.Value);
            Assert.NotNull(marca);
            Assert.Equal(2, marca.Id);
            Assert.Equal("Adidas", marca.Nombre);
        }

        [Fact]
        public async Task MarcaController_GetItem_ReturnNotFound()
        {
            // Arrange
            MarketDbContext dbContext = await GetDbContext();
            MarcaService service = new MarcaService(dbContext);
            MarcaController controller = new MarcaController(service);

            // Act
            ActionResult<MarcaResponse> result = await controller.Get(4);

            // Asert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task MarcaController_CreateItem_ReturnMarcaResponse()
        {
            // Arrange
            MarketDbContext dbContext = await GetDbContext();
            MarcaService service = new MarcaService(dbContext);
            MarcaController controller = new MarcaController(service);
            CreateMarcaRequest request = new CreateMarcaRequest() { Nombre = "Rebook" };

            // Act
            ActionResult<MarcaResponse> result = await controller.Post(request);

            // Asert
            CreatedAtActionResult objectResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            MarcaResponse marca = Assert.IsType<MarcaResponse>(objectResult.Value);
            Assert.NotNull(marca);
            Assert.Equal("Rebook", marca.Nombre);
        }
    }
}
