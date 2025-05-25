using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SurfTicket.Infrastructure.Repository.Interface;
using SurfTicket.Infrastructure.Data;
using SurfTicket.Domain.Models;

namespace SurfTicket.Infrastructure.Repository
{
    public class EfUnitOfWork : IEfUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public EfUnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();

            }
            _dbContext.Dispose();
        }

        public async Task SaveChangesAsync(string auditUserId = "System")
        {
            var entries = _dbContext.ChangeTracker.Entries<EntityAudit>();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = utcNow;
                    entry.Entity.UpdatedAt = utcNow;
                    entry.Entity.CreatedBy = auditUserId;
                    entry.Entity.UpdatedBy = auditUserId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.CreatedAt).IsModified = false;
                    entry.Property(x => x.CreatedBy).IsModified = false;

                    entry.Entity.UpdatedAt = utcNow;
                    entry.Entity.UpdatedBy = auditUserId;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
