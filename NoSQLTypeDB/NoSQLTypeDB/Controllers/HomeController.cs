using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoSQLTypeDB.Controllers;

public class HomeController : Controller
{
    private readonly DocumentService _service;

    public HomeController(DocumentService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var documents = _service.GetByPredicate();
        return View(documents);
    }

    [HttpPost]
    public IActionResult AddDocument([FromBody] DocumentModel document)
    {
        try
        {
            var bsonDocument = BsonDocument.Parse(document.FieldText);
            _service.Add(bsonDocument);
            return Json(new { success = true });  // Return a JSON response
        }
        catch (FormatException ex)
        {
            return BadRequest(new { message = "Invalid JSON format", error = ex.Message });
        }
    }

    [HttpDelete]
    public IActionResult DeleteDocument(string id)
    {
        try
        {
            var objectId = new ObjectId(id);
            _service.DeleteById(objectId);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Error deleting document", error = ex.Message });
        }
    }

    [HttpPut]
    public IActionResult UpdateDocument(string id, [FromBody] DocumentModel document)
    {
        Console.WriteLine(id);
        try
        {
            var objectId = new ObjectId(id);
            var bsonDocument = BsonDocument.Parse(document.FieldText);
            _service.UpdateById(objectId, bsonDocument);
            return Json(new { success = true });
        }
        catch (FormatException ex)
        {
            return BadRequest(new { message = "Error updating document", error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Error updating document", error = ex.Message });
        }
    }
}


public class DocumentModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("fieldText")]
    public string FieldText { get; set; }
}
