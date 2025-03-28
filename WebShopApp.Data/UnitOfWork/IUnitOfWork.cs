namespace WebShopApp.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        // kaç kayda etki ettiğini geriye döner, o yüzden int.

        Task BeginTransaction();

        Task CommitTransaction();

        Task RollBackTransaction();
    }
}