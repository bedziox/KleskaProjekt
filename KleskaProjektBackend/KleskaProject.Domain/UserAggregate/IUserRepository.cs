namespace KleskaProject.Domain.UserAggregate;

public interface IUserRepository
{
    User GetById(Guid id);
    Task<User> GetByEmail(string email);
    bool DeleteById(Guid id);
    IEnumerable<User> GetAll();
    Task<IEnumerable<User>> GetAllAsync();
    void Add(User user);
    bool IsEmailTaken(string email);
}
