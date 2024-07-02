using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var documents = new List<object>
            {
                new { Id = 1, Field = "asdsa", Weight = 50 },
                new { Id = 2, Sex = "male" },
                new { Id = 3, Name = "John Doe", Age = 30, Active = true }
            };

            return View(documents);
        }

        [HttpPost]
        public IActionResult AddDocument([FromBody] DocumentModel document)
        {
            if (ModelState.IsValid)
            {
                // Обробка документа
                return Json(new { success = true, field = document.FieldText, message = "Document added successfully" });
            }

            return BadRequest(new { success = false, message = "Invalid data" });
        }
    }
    public class DocumentModel
    {
        public string FieldText { get; set; }
    }

}
