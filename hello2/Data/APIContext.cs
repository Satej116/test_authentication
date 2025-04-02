using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace hello2.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        public DbSet<ApageTable> ApageTable { get; set; }
    }

    public class ApageTable
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
    }
}
