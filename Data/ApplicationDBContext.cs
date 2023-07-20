using BitirmeProj.Models;
using Microsoft.EntityFrameworkCore;


namespace BitirmeProj.Data
{
    public class ApplicationDBContext :DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
        {
            
        }
        public DbSet<Person> Persons { get; set; }
       
    }
}