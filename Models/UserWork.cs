using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class UserWork
    {
        [Key]
        public int UserWorksID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FinishDate { get; set; }
        public string Title { get; set; }
        public string? Institution { get; set; }

        public string? Description { get; set; }

       //public User User { get; set; }

    }

}
