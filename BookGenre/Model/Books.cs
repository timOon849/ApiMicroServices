using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookGenre.Model
{
    public class Books
    {
        [Key]
        public int ID_Book { get; set; }

        public required string Name { get; set; }
        public string? Author { get; set; }
        [DataType(DataType.Date)]
        public DateTime YearOfIzd { get; set; }
        public string? Description { get; set; }

        public int ID_Genre { get; set; }
        [ForeignKey("ID_Genre")]
        public virtual Genre Genre { get; set; }
    }
}
