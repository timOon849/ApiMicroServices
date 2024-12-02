using System.ComponentModel.DataAnnotations;

namespace ApiMicroServices.DB
{
    public class Image
    {
        [Key]
        public int Id_image { get; set; }
        public byte[] File { get; set; }
        public string? ContentType { get; set; }
        public DateTime Create_date { get; set; }
    }
}
