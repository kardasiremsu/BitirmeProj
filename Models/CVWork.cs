using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CVWork
    {
        [Key]
        public int CVWorksID { get; set; }

        public int CVID { get; set; }
        public int StarDate { get; set; }
        public int FinishDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Foreign key
        [ForeignKey("CVID")]
        public CV CV { get; set; }
    }
    
}
