using System.Collections.Generic;

namespace ProjectZero.Areas.FileExplorerAdmin.Models {
    public class FileExplorerFolderModel {
        public List <FolderModel> GivenFolderList { get; set; }
        public List <FolderModel> PathFolderList { get; set; }
        public int CurrParentFolder { get; set; }
    }
}