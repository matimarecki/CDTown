using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectZero.Models {
    public class BookModel {
        public string Name { get; set; }
        public int Pages { get; set; }
        public int ReleaseYear { get; set; }
        public ICollection<LibraryConnectionModel> Connections { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}