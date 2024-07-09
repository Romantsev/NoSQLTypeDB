using BLL.Services;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using NoSQLTypeDB.Controllers;

namespace ModuleTest
{
    public class UnitTest1
    {
        private readonly Mock<IRepository<BsonDocument>> _mockRepository;
        private readonly DocumentService _mockService;
        private readonly HomeController _controller;

        public UnitTest1()
        {
            _mockRepository = new Mock<IRepository<BsonDocument>>();
            _mockService = new DocumentService(_mockRepository.Object);
            _controller = new HomeController(_mockService);
        }

        [Fact]
        public void Index_Returns_ViewResult_With_Documents()
        {
            // Arrange
            var documents = new List<BsonDocument> { new BsonDocument("fieldText", "Sample text") };
            _mockRepository.Setup(repo => repo.GetAllAsList()).Returns(documents);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BsonDocument>>(viewResult.ViewData.Model);
            Assert.Equal(documents, model);
        }

        [Fact]
        public void AddDocument_Returns_JsonResult_On_Success()
        {
            // Arrange
            var document = new DocumentModel { FieldText = "{\"fieldText\": \"Sample text\"}" };

            // Act
            var result = _controller.AddDocument(document);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var success = jsonResult.Value.GetType().GetProperty("success").GetValue(jsonResult.Value, null);
            Assert.True((bool)success);
        }

        [Fact]
        public void AddDocument_Returns_BadRequest_On_InvalidJson()
        {
            // Arrange
            var document = new DocumentModel { FieldText = "Invalid JSON" };

            // Act
            var result = _controller.AddDocument(document);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = badRequestResult.Value.GetType().GetProperty("message").GetValue(badRequestResult.Value, null);
            Assert.Equal("Invalid JSON format", errorMessage);
        }

        [Fact]
        public void DeleteDocument_Returns_JsonResult_On_Success()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = _controller.DeleteDocument(id);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var success = jsonResult.Value.GetType().GetProperty("success").GetValue(jsonResult.Value, null);
            Assert.True((bool)success);
        }

        [Fact]
        public void DeleteDocument_Returns_BadRequest_On_Error()
        {
            // Arrange
            var id = "Invalid ObjectId";

            // Act
            var result = _controller.DeleteDocument(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = badRequestResult.Value.GetType().GetProperty("message").GetValue(badRequestResult.Value, null);
            Assert.Equal("Error deleting document", errorMessage);
        }

        [Fact]
        public void UpdateDocument_Returns_JsonResult_On_Success()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var document = new DocumentModel { FieldText = "{\"fieldText\": \"Updated text\"}" };

            // Act
            var result = _controller.UpdateDocument(id, document);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var success = jsonResult.Value.GetType().GetProperty("success").GetValue(jsonResult.Value, null);
            Assert.True((bool)success);
        }

      
        

    }
}