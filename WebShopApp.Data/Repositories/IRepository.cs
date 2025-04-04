using System.Linq.Expressions;

namespace WebShopApp.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // jenerik bir yapı kullanılasının sebebi bütün entity class ları ile db arasında iletişim kurmak için.
        void Add(TEntity entity);
        void Delete(TEntity entity, bool isHardDelete = true);
        void Delete(int id);
        void Update(TEntity entity);
        TEntity GetById(int id);

        // Advance
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
    }
}