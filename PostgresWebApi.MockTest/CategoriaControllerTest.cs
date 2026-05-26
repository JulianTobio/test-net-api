using Microsoft.AspNetCore.Mvc;
using Moq;
using PostgresWebApi.Controllers;
using PostgresWebApi.Dtos;
using PostgresWebApi.Services;

namespace PostgresWebApi.MocTest
{
    public class CategoriaControllerTest
    {
        private IList<CategoriaResponse> GetTestCategorias()
        {
            return [
                new CategoriaResponse() { Id = 1, Nombre = "Ropa de Varon" },
                new CategoriaResponse() { Id = 2, Nombre = "Ropa de Mujer" },
                new CategoriaResponse() { Id = 3, Nombre = "Ropa de Nino" }
            ];
        }
        [Fact]
        public async Task CategoriaController_GetItems_ReturnCategoriaResponseArray()
        {
            // Arrange
            var serviceMock = new Mock<ICategoriaService>();
            serviceMock.Setup(s => s.GetCategoriasAsync()).Returns(Task.FromResult(GetTestCategorias()));
            CategoriaController controller = new CategoriaController(serviceMock.Object);

            // Act
            ActionResult<IEnumerable<CategoriaResponse>> result = await controller.GetItems();

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            IEnumerable<CategoriaResponse> categorias = Assert.IsType<IEnumerable<CategoriaResponse>>(objectResult.Value, exactMatch: false);
            Assert.NotEmpty(categorias);
            Assert.Equal(3, categorias.Count());
        }

        [Fact]
        public async Task MarcaController_GetItem_ReturnMarcaResponse()
        {
            // Arrange
            int testItemId = 1;
            var serviceMock = new Mock<ICategoriaService>();
            serviceMock.Setup(s => s.GetCategoriaByIdAsync(testItemId)).Returns(Task.FromResult(new CategoriaResponse() { Id = testItemId, Nombre = "Ropa de Varon "}));
            CategoriaController controller = new CategoriaController(serviceMock.Object);

            // Act
            ActionResult<CategoriaResponse> result = await controller.Get(testItemId);

            // Asert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            CategoriaResponse categoria = Assert.IsType<CategoriaResponse>(objectResult.Value);
            Assert.NotNull(categoria);
            Assert.Equal(1, categoria.Id);
        }

        [Fact]
        public async Task MarcaController_GetItem_ReturnNotFound()
        {
            int testItemId = 4;
            var serviceMock = new Mock<ICategoriaService>();
            serviceMock.Setup(s => s.GetCategoriaByIdAsync(testItemId)).Returns(Task.FromResult<CategoriaResponse?>(null));
            CategoriaController controller = new CategoriaController(serviceMock.Object);

            // Act
            ActionResult<CategoriaResponse> result = await controller.Get(testItemId);

            // Asert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task MarcaController_CreateItem_ReturnMarcaResponse()
        {
            // Arrange
            CreateCategoriaRequest request = new CreateCategoriaRequest() { Nombre = "Ropa de Mujer" };
            var serviceMock = new Mock<ICategoriaService>();
            serviceMock.Setup(s => s.AddCategoriaAsync(request)).Returns(Task.FromResult(new CategoriaResponse() { Id = 2, Nombre = request.Nombre }));
            CategoriaController controller = new CategoriaController(serviceMock.Object);

            // Act
            ActionResult<CategoriaResponse> result = await controller.Post(request);

            // Asert
            CreatedAtActionResult objectResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            CategoriaResponse categoria = Assert.IsType<CategoriaResponse>(objectResult.Value);
            Assert.NotNull(categoria);
            Assert.Equal(request.Nombre, categoria.Nombre);
        }

        [Fact]
        public async Task MarcaController_CreateItem_ReturnNoContent()
        {
            // Arrange
            int itemId = 1;
            UpdateCategoriaRequest request = new UpdateCategoriaRequest() { Id = itemId, Nombre = "Ropa de Mujer" };
            var serviceMock = new Mock<ICategoriaService>();
            serviceMock.Setup(s => s.UpdateCategoriaAsync(itemId, request)).Returns(Task.FromResult(true));
            CategoriaController controller = new CategoriaController(serviceMock.Object);

            // Act
            ActionResult<CategoriaResponse> result = await controller.Put(itemId, request);

            // Asert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task MarcaController_CreateItem_ReturnNotFound()
        {
            // Arrange
            int itemId = 4;
            UpdateCategoriaRequest request = new UpdateCategoriaRequest() { Id = itemId, Nombre = "Ropa de Bebe" };
            var serviceMock = new Mock<ICategoriaService>();
            serviceMock.Setup(s => s.UpdateCategoriaAsync(itemId, request)).Returns(Task.FromResult(false));
            CategoriaController controller = new CategoriaController(serviceMock.Object);

            // Act
            ActionResult<CategoriaResponse> result = await controller.Put(itemId, request);

            // Asert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task MarcaController_DeleteItem_ReturnNoContent()
        {
            // Arrange
            int itemId = 1;
            var serviceMock = new Mock<ICategoriaService>();
            serviceMock.Setup(s => s.DeleteCategoriaAsync(itemId)).Returns(Task.FromResult(true));
            CategoriaController controller = new CategoriaController(serviceMock.Object);

            // Act
            ActionResult<CategoriaResponse> result = await controller.Delete(itemId);

            // Asert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task MarcaController_DeleteItem_ReturnNotFound()
        {
            // Arrange
            int itemId = 4;
            var serviceMock = new Mock<ICategoriaService>();
            serviceMock.Setup(s => s.DeleteCategoriaAsync(itemId)).Returns(Task.FromResult(false));
            CategoriaController controller = new CategoriaController(serviceMock.Object);

            // Act
            ActionResult<CategoriaResponse> result = await controller.Delete(itemId);

            // Asert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
