using InternService.Api.Context;
using InternService.Api.Model;
using Microsoft.EntityFrameworkCore;
using static InternService.Api.Services.IGenericCrudInterface;

namespace InternService.Api.Manager
{
    public class CrudGenericRepository<T> : IGenericService<T> where T : class
    {
        private InternDbContext _dbContext;
        DbSet<T> values;

        public CrudGenericRepository()
        {
            values = _dbContext.Set<T>();
        }
        public void INAdd(T t)
        {
            values.Add(t);
            _dbContext.SaveChanges();
        }

        public void INDelete(T t)
        {
            values.Remove(t);
            _dbContext.SaveChanges();
        }

        public void INGetAccept(T t)
        {
            if (t is InternStatus internStatus)
            {

                internStatus.InternAccept = true;
                _dbContext.SaveChanges();
            }
        }

        public T INGetById(int id)
        {
            return values.Find(id);
        }

        public void INGetConfirm(T t)
        {
            if (t is InternStatus internStatus)
            {
                internStatus.InternConfirm = true;
                _dbContext.SaveChanges();
            }
        }

        public void INGetContributConfirm(T t)
        {
            if (t is InternStatus internStatus)
            {
                internStatus.ContributConfirm = true;
                _dbContext.SaveChanges();
            }
        }

        public void INGetContributDecline(T t)
        {
            if (t is InternStatus internStatus)
            {
                internStatus.ContributConfirm = false;
                _dbContext.SaveChanges();
            }
        }

        public void INGetDecline(T t)
        {
            if (t is InternStatus internStatus)
            {
                internStatus.InternConfirm = false;
                _dbContext.SaveChanges();
            }
        }

        public List<T> INGetListAll()
        {
            return values.ToList();
        }

        public void INGetReject(T t)
        {
            if (t is InternStatus internStatus)
            {
                internStatus.InternAccept = false;

                var sebepProperty = typeof(T).GetProperty("RejectReason");
                if (sebepProperty != null)
                {
                    var sebepValue = sebepProperty.GetValue(t);
                    internStatus.RejectReason = sebepValue?.ToString();
                }
                _dbContext.SaveChanges();
            }
            //if (t is InternStatus internStatus)
            //{

            //    internStatus.InternAccept = false;
            //    values.Add(t);
            //    _dbContext.SaveChanges();
            //}
        }


        public void INUpdate(T t)
        {
            values.Update(t);
            _dbContext.SaveChanges();
        }
    }
}
