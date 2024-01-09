using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CVReference
    {
        [Key]
        public int CVReferencesID { get; set; }

        public int CVID { get; set; }
        public string ReferenceFullName { get; set; }
        public string ReferenceMobile { get; set; }
        public string ReferenceMail { get; set; }
        public string ReferenceTitle { get; set; }

        // Foreign key
        [ForeignKey("CVID")]
        public CV CV { get; set; }
    
    }
}
