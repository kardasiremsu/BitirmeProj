using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CVWork
    {
        [Key]
        public int CVWorksID { get; set; }

        public int CVID { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Title { get; set; }
        public string? Institution { get; set; }

        public string? Description { get; set; }

        // Foreign key
        //[ForeignKey("CVID")]
        //public CV CV { get; set; }
    }
    
}
