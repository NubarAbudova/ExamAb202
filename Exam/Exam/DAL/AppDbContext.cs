using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
    }
}
