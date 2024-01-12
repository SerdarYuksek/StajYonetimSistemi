using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
    // Core katmanında tanımlanan generic interfacenin metodlarının yazılması
    public class CrudGenericRepository<T> : IGenericCrudInterface<T> where T : class
    {
        private SurveyDbContext _dbContext;
        DbSet<T> values;

        public CrudGenericRepository(SurveyDbContext dbContext)
        {
            _dbContext = dbContext;
            values = _dbContext.Set<T>();
        }

        public void SAdd(T t)
        {
            values.Add(t);
            _dbContext.SaveChanges();
        }

        public void SDelete(T t)
        {
            values.Remove(t);
            _dbContext.SaveChanges();
        }

        public T SGetById(int? id)
        {
            return values.Find(id);
        }

        public List<T> SGetListAll()
        {
            return values.ToList();
        }

        public void SUpdate(T t)
        {
            values.Update(t);
            _dbContext.SaveChanges();
        }
    }
}
