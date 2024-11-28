using System.ComponentModel.DataAnnotations;

namespace ReadersAndRent.Model
{
    public class Readers
    {
        [Key]
        public int Id_Reader { get; set; }
        public string? Name { get; set; }
        public string? FName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }
    }
}
