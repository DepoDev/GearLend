using System.Linq;
using System.Threading.Tasks;

namespace GearLend.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        IQueryable<T> Get();
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
