using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProvaAdmissionalCSharpApisul.Infra.Data.Contexts
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
    }
}