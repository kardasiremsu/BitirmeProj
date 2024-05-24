using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class UserReference
    {
        [Key]
        public int UserReferencesID { get; set; }

        public int UserID { get; set; }
        public string ReferenceFullName { get; set; }
        public string ReferenceMobile { get; set; }
        public string ReferenceMail { get; set; }
        public string ReferenceTitle { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

    }
}
