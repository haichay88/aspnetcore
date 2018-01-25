using System;
using System.Collections.Generic;
using System.Text;

namespace tube.Data
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Call save change from db context
        /// </summary>
        void Commit();
        IRepository<T> Repository<T>() where T : class;
    }
}
