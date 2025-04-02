using Microsoft.EntityFrameworkCore;
using Authentication.Models;
using System.Net;

namespace Authentication.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        // Change 'Client' to 'Client2' 
        public DbSet<Apage> ApageTable { get; set; }
    }
}

