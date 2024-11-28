using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookGenre.Model;

namespace ReadersAndRent.Model
{
    public class Rent
    {
        [Key]
        public int ID_History { get; set; }

        [DataType(DataType.Date)]
        public required DateTime Date_Start { get; set; }
        public DateTime? Date_End { get; set; }
        public int Srok { get; set; }
        [ForeignKey("Book")]
        public int ID_Book { get; set; }
        public Books Book { get; set; }

        [ForeignKey("Readers")]
        public int ID_Reader { get; set; }
        public Readers Readers { get; set; }
    }
}
