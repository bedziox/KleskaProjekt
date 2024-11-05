namespace KleskaProjekt.Domain.Common.Shared
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

    }
}
