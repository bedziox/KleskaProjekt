using KleskaProject.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace KleskaProject.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public bool DeleteById(Guid id)
    {
        var user = _context.Users.FirstOrDefault(db => db.Id == id);
        if (user == null)
        {
            return false;
        }
        _context.Users.Remove(user);
        return true;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    async public Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public User GetById(Guid id)
    {
        return (User)_context.Users.Where(db => db.Id == id);
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.Where(db => db.Email == email).SingleOrDefaultAsync();
    }

    public bool IsEmailTaken(string email)
    {
        return _context.Users.Where(db => db.Email == email) != null;
    }

}
