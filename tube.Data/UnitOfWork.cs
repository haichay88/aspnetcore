using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tube.Data
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly TubeDbContext _context;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
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
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IRepository<TEntity>;
            }
           
            IRepository<TEntity> repo = new Repository<TEntity>(_context);
            repositories.Add(typeof(TEntity), repo);
            return repo;
        }
    }
}
