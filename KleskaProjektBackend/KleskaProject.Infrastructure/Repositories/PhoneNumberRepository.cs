using KleskaProject.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace KleskaProjekt.Infrastructure.Repositories
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public PhoneNumberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PhoneNumber> GetByIdAsync(Guid id)
        {
            return await _context.PhoneNumbers.Where(db => db.Id == id).FirstOrDefaultAsync();
        }

        public void Add(PhoneNumber phoneNumber)
        {
            _context.PhoneNumbers.Add(phoneNumber);
        }

        public async Task UpdateAsync(PhoneNumber phoneNumber)
        {
            _context.PhoneNumbers.Update(phoneNumber);
        }

        public async Task DeleteAsync(Guid id)
        {
            var phoneNumber = await GetByIdAsync(id);
            if (phoneNumber != null)
            {
                _context.PhoneNumbers.Remove(phoneNumber);
            }
        }
    }
}
