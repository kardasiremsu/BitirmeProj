using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CV
    {
        [Key]
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string? Skills { get; set; }

        public string? CoverLetter { get; set; }

    }
}
