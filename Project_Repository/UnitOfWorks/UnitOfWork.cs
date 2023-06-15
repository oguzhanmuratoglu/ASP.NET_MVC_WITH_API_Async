using Project_Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _context;
        public UnitOfWork(ProjectDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
