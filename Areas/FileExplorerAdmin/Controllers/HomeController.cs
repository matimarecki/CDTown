using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectZero.Areas.FileExplorerAdmin.Models;
using ProjectZero.Areas.FileExplorerAdmin.Services;

namespace ProjectZero.Areas.FileExplorerAdmin.Controllers{
    [Area("FileExplorerAdmin")]
    [Route("FileExplorerAdmin")]
    public class HomeController : Controller {
        private readonly IFolderService _service;
        // private readonly IFileExplorerPathingService _pathingService;
        public HomeController (IFolderService service) {
            // this._pathingService = pathingService;
            this._service = service;
        }
        
        

        public IActionResult Index(int currId = 0) {
            FileExplorerFolderModel fileExplorer = new FileExplorerFolderModel();
            fileExplorer.GivenFolderList = this._service.ShowFolders(currId);
            fileExplorer.CurrParentFolder = currId;
            fileExplorer.PathFolderList = this._service.ShowPathToFolder(currId);
            return View("Index",fileExplorer);
        }
        
        [Route("FolderAdder")]
        public IActionResult FolderAdder(int papaFolderId) {
            Console.WriteLine(papaFolderId);
            return View(papaFolderId);
        }

        [Route("FolderEditor")]
        public IActionResult FolderEditor(int editedFolderId) {
            FolderModel inspectedFolder = this._service.GetSpecificFolder(editedFolderId);
            return View(inspectedFolder);
        }

        [Route("EditFolder")]
        public IActionResult EditFolder(FolderModel editedFolder) {
            FolderModel helperFolderModel = this._service.GetSpecificFolder(editedFolder.Id);
            helperFolderModel.FolderName = editedFolder.FolderName;
            helperFolderModel.Accessibility = editedFolder.Accessibility;
            this._service.EditFolder(helperFolderModel);
            Console.WriteLine(helperFolderModel.ParentId);
            return RedirectToAction("Index", new{currId = helperFolderModel.ParentId});
        }
        [Route("FolderCounter")]
        public IActionResult FolderCounter() {
            Console.WriteLine(this._service.CountMyFolders());
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("AddFolder")]
        public IActionResult AddFolder(string folderName, string accessibility, int parentId) {
            FolderModel addedFolder = new FolderModel();
            if (parentId == 0) {
                addedFolder.Parent = null;
                addedFolder.ParentId = null;
            }
            else {
                addedFolder.Parent = this._service.GetSpecificFolder(parentId);
                addedFolder.ParentId = parentId;
            }
            addedFolder.Accessibility = accessibility;
            addedFolder.DateCreated = DateTime.Now;
            addedFolder.FolderName = folderName;
            this._service.AddFolder(addedFolder);
            return RedirectToAction("Index", new {currId = parentId});
        }
        
        [Route("MoveDownToFolder")]
        public IActionResult MoveDownToFolder(int destinationFolderId) {
            // this._pathingService.AddFolderToCurrentPath(this._service.GetSpecificFolder(destinationFolderId));
            return RedirectToAction("Index", new {currId = destinationFolderId});
        }

        [Route("MoveUpOneFolder")]
        public IActionResult MoveUpOneFolder(int currentId) {
            if (currentId == 0) {
                return RedirectToAction("Index");
            }
            if (this._service.GetSpecificFolder(currentId).ParentId == null) {
                return RedirectToAction("Index");
            }
            int currFatherId = (int)this._service.GetSpecificFolder(currentId).ParentId;
            Console.WriteLine(currFatherId);
            return RedirectToAction("Index", new{currId = currFatherId});
            //Does it work? don't touch it
        }
        
        [Route("CleanseEverything")]
        public IActionResult CleanseEverything() {
            this._service.CleanseEverything();
            return RedirectToAction("Index");
        }

        [Route("RemoveFolder")]
        public IActionResult RemoveFolder(int folderId, int fatherId) {
            if (this._service.ShowFolders(folderId).Count == 0) {
                this._service.RemoveFolder(folderId);
                return RedirectToAction("Index", new{currId = fatherId});
            }
            else {
                Console.WriteLine("Element still contains other Folders, please make sure the element is empty");
                return RedirectToAction("Index", new{currId = fatherId});
            }
        }

        [Route("ShowPath")]
        public IActionResult ShowPath(int currentId) {
            List<FolderModel> pathList = this._service.ShowPathToFolder(currentId);
            foreach (var pathFolder in pathList) {
                Console.WriteLine(pathFolder.FolderName);
            }
            return RedirectToAction("Index", new {currId = currentId});
        }
        // public IActionResult MoveUpOneFolder(int parentId) {
        //     this._pathFolderList.RemoveAt(this._pathFolderList.Count - 1);
        //     return RedirectToAction("Index", parentId);
        // }
        // public IActionResult MoveUpToFolderByPath(int thatFolderId) {
        //     List<FolderModel> belowThatFolder = this._pathFolderList
        //         .Where(n => n.Id > thatFolderId).ToList();
        //     foreach (var movingUpInPath in belowThatFolder) {
        //         this._pathFolderList.Remove(movingUpInPath);
        //     }
        //     return RedirectToAction("Index", thatFolderId);
        // }
        [Route("ReturnToRootFolder")]
        public IActionResult ReturnToRootFolder() {
            // foreach (var getRidOfIt in this._pathingService.ShowCurrentPath()) {
            //     this._pathingService.RemoveFolderFromCurrentPath(getRidOfIt);
            // }
            return RedirectToAction("Index");
        }
    }
}