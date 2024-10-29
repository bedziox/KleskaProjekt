using KleskaProject.Infrastructure.Persistence;
using KleskaProjekt.Domain.Common.Shared;

namespace KleskaProjekt.Infrastructure.Extensions
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
