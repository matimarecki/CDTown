using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectZero.Models {
    public class LibraryConnectionModel {
        public BookModel Book { get; set; }
        public AuthorModel Author { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}