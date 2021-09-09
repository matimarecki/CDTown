using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectZero.Models {
    public class AuthorModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ICollection<LibraryConnectionModel> Connections { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}