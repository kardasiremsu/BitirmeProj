using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CV
    {
        [Key]
        public int CVID { get; set; }

        public int UserID { get; set; }
        public byte[]? CVFile { get; set; } // Replace with appropriate data type
        public int LastUpdateDate { get; set; }
        public int CreationDate { get; set; }
        public string CVName { get; set; }

        // Foreign key
        [ForeignKey("UserID")]
        public User User { get; set; }

       
    }
}
