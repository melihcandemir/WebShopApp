
using Microsoft.EntityFrameworkCore.Storage;
using WebShopApp.Data.Context;

namespace WebShopApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebShopAppDbContext _db;
        private IDbContextTransaction _transaction;

        public UnitOfWork(WebShopAppDbContext db)
        {
            _db = db;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
