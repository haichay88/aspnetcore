using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tube.Data
{
    public class DbInitializer
    {
        private readonly TubeDbContext _context;

        public DbInitializer(TubeDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            await _context.SaveChangesAsync();
        }
    }
}
