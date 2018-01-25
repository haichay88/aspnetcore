using System;
using System.Collections.Generic;
using System.Text;

namespace tube.Data
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly TubeDbContext _context;
        public EFUnitOfWork(TubeDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
