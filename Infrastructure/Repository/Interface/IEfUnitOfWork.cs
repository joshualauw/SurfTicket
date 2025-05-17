namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface IEfUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
