using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class Application
    {
        [Key]
        public int ID { get; set; }
        public int AdvertID { get; set; }

    }
}
