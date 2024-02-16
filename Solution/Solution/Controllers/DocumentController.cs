using Microsoft.AspNetCore.Mvc;
using Solution.Models;
using Solution.Services.Repositories;
using System.Net;

namespace Solution.Controllers
{
    public class DocumentController(IDocumentRepository documentRepository) : BaseController
    {
        private readonly IDocumentRepository _documentRepository = documentRepository;


        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(GetUserId());
            var response = await _documentRepository.List(userId);
            return View(response);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            if (file == null)
            {
                SetAlert("Invalid request", AlertType.Danger);
                return View();
            }

            var userId = Guid.Parse(GetUserId());
            var response = await _documentRepository.Upload(file, userId);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                SetAlert(response.Message, AlertType.Danger);
                return View();
            }

            SetAlert(response.Message, AlertType.Success);
            return RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
