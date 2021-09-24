using System.Collections.Generic;
using ProjectZero.Areas.FileExplorerAdmin.Models;

namespace ProjectZero.Areas.FileExplorerUser.Models {
    public class FileExplorerUserModel {
        public FileExplorerFolderModel TheFolders { get; set; }
        public List<FileUploaderModel> TheFiles { get; set; }
    }
}