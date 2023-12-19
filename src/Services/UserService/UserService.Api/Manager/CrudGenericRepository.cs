using Microsoft.EntityFrameworkCore;
using UserService.Api.Context;
using UserService.Api.Services;

namespace UserService.Api.Manager
{
    //User Servisindeki CRUD İşlemlerin Generic Yapı ile metodların Yazılması ve İnterfacenin implamente edilmesi
    public class CrudGenericRepository<T> : IGenericService<T> where T : class
    {
        private UserDbContext _dbContext;
        DbSet<T> values;

        public CrudGenericRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
            values = _dbContext.Set<T>();
        }
        public List<T> UGetListAll()
        {
            return values.ToList();
        }

        public void UAdd(T t)
        {
            values.Add(t);
            _dbContext.SaveChanges();
        }

        public void UDelete(T t)
        {
            values.Remove(t);
            _dbContext.SaveChanges();
        }

        public T UGetById(int id)
        {
            return values.Find(id);
        }

        public void UUpdate(T t)
        {
            values.Update(t);
            _dbContext.SaveChanges();
        }
    }
}
