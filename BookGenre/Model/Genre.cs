using System.ComponentModel.DataAnnotations;

namespace BookGenre.Model
{
    public class Genre
    {
        [Key]
        public int ID_Genre { get; set; }

        public string? Name_Genre { get; set; }
    }
}
