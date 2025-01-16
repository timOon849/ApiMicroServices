using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiMicroServices.DB
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
        public Image Image { get; set; }

        [ForeignKey("Zhanr")]
        public int ID_Genre { get; set; }
        public Genre Genre { get; set; }
    }
}
