using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjectZero.Areas.FileExplorerAdmin.Models;
using ProjectZero.Areas.FileExplorerAdmin.Services;
using ProjectZero.Areas.FileExplorerUser.Models;
using ProjectZero.Areas.FileExplorerUser.Services;

namespace ProjectZero.Areas.FileExplorerUser.Controllers {
    [Area("FileExplorerUser")]
    [Route("FileExplorerUser")]
    public class HomeController : Controller {
        private readonly IUserFolderService _service;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IUserFileService _fileService;

        public HomeController(IUserFolderService service, IWebHostEnvironment webHostEnvironment, IUserFileService fileService) {
            this._fileService = fileService;
            this._service = service;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int currId = 0) {
            List<FileUploaderModel> daFilez = this._fileService.ReturnFilesInFolder(currId);
            
            FileExplorerFolderModel fileExplorer = new FileExplorerFolderModel();
            FileExplorerUserModel fullSet = new FileExplorerUserModel();
            fullSet.TheFiles = daFilez;
            fileExplorer.GivenFolderList = this._service.ShowFolders(currId);
            fileExplorer.CurrParentFolder = currId;
            fileExplorer.PathFolderList = this._service.ShowPathToFolder(currId);
            fullSet.TheFolders = fileExplorer;
            return View("Index", fullSet);
        }

        [HttpPost]
        [Route("Upload")]
        public IActionResult Upload(IFormFile fileInput, int fatherId) {
            string uploads = Path.Combine(this._webHostEnvironment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploads)) {
                Directory.CreateDirectory(uploads);
            }
            Console.WriteLine(fileInput.Name);
            this._fileService.AddFileToFolder(fatherId, fileInput);
            return RedirectToAction("Index", new{currId = fatherId});
        }
        [Route("MoveUpOneFolder")]
        public IActionResult MoveUpOneFolder(int currentId) {
            if (currentId == 0) {
                return RedirectToAction("Index");
            }

            if (this._service.GetSpecificFolder(currentId).ParentId == null) {
                return RedirectToAction("Index");
            }

            int currFatherId = (int) this._service.GetSpecificFolder(currentId).ParentId;
            Console.WriteLine(currFatherId);
            return RedirectToAction("Index", new {currId = currFatherId});
            //Does it work? don't touch it
        }

        [Route("ShowPath")]
        public IActionResult ShowPath(int currentId) {
            List<FolderModel> pathList = this._service.ShowPathToFolder(currentId);
            foreach (var pathFolder in pathList) {
                Console.WriteLine(pathFolder.FolderName);
            }

            return RedirectToAction("Index", new {currId = currentId});
        }

        [Route("CreateNewFile")]
        public IActionResult CreateNewFile(int fatherId,IFormFile stream) {
            Console.WriteLine(stream.ContentType);
            return RedirectToAction("Index", fatherId);
        }

        [Route("DelteFile")]
        public IActionResult DelteFile(int fatherId, int deltedId) {
            this._fileService.RemoveFile(deltedId);
            return RedirectToAction("Index", new {currId = fatherId});
        }
        [Route("DownloadFile")]
        public FileResult DownloadFile(string fileName) {
            string path = Path.Combine(this._webHostEnvironment.WebRootPath, "Uploads/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "appliation/octet-stream", fileName);
        }
    }
}