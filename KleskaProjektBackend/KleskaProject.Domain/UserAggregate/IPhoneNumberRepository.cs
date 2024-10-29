namespace KleskaProject.Domain.UserAggregate
{
    public interface IPhoneNumberRepository
    {
        Task<PhoneNumber> GetByIdAsync(Guid id);
        void Add(PhoneNumber phoneNumber);
        Task UpdateAsync(PhoneNumber phoneNumber);
        Task DeleteAsync(Guid id);
    }
}
