using ExamTicketTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamTicketTwo.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
     public   DbSet<TeamMember> TeamMembers { get; set; }
        


}
}
