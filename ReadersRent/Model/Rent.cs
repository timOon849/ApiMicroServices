using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ReadersRent.Model
{
    public class Rent
    {
        [Key]
        public int ID_Rent { get; set; }

        [DataType(DataType.Date)]
        public required DateTime Date_Start { get; set; }
        public DateTime? Date_End { get; set; }
        public int Srok { get; set; }
        public int ID_Reader { get; set; }
        public int ID_Book { get; set; }
    }
}
