using BitirmeProj.Models;


namespace BitirmeProj.Data
{
    public class ApplicationDBContext :DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
        {
            
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Person> Persons { get; set; }
       
    }
}


