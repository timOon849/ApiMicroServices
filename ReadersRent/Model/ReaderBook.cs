using System.ComponentModel.DataAnnotations;

namespace ReadersRent.Model
{
    public class ReaderBook
    {
        [Key]
        public int ID_ReaderBook { get; set; }
        public int ID_Book { get; set; }
        public int ID_Reader { get; set; }
    }
}
