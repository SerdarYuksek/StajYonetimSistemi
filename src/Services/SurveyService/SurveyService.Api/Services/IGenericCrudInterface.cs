using Microsoft.EntityFrameworkCore;
using SurveyService.Api.Context;

namespace SurveyService.Api.Services
{
    //Survey servisindeki CRUD İşlemlerin Generic Yapı ile İnterfacesinin Yazılması
    public interface IGenericCrudInterface<T>
    {

        void SAdd(T t);
        void SDelete(T t);
        void SUpdate(T t);
        List<T> SGetListAll();
        T SGetById(int? id);

    }


    //Survey Servisindeki CRUD İşlemlerin Generic Yapı ile metodların Yazılması ve İnterfacenin implamente edilmesi
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


