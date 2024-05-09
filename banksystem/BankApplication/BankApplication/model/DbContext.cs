using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.model
{
    public class BankDbContext : DbContext
    {
        public DbSet<User1> Users { get; set; }

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
            Users = Set<User1>();
        }
    }
}
