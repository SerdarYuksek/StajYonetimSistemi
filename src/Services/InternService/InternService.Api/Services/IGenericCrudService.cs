using InternService.Api.Context;
using InternService.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace InternService.Api.Services
{
    //Intern Servisindeki CRUD İşlemlerin Generic Yapı ile İnterfacesinin Yazılması
    public interface IGenericCrudService<T>
    {

        void INAdd(T t);
        void INDelete(T t);
        void INUpdate(T t);
        List<T> INGetListAll();
        void INGetConfirm(InternStatus internship);
        void INGetAccept(InternStatus internship, int acceptDays);
        void INGetConfirmDecline(InternStatus internship);
        void INGetReject(InternStatus internship, string rejectReason);
        void INGetAcceptDecline(InternStatus internship);
        void INGetContributConfirm(InternStatus internship);
        void INGetContributDecline(InternStatus internship);
        T INGetById(int id);

    }

    //User Servisindeki CRUD İşlemlerin Generic Yapı ile metodların Yazılması ve İnterfacenin implamente edilmesi
    public class CrudGenericRepository<T> : IGenericCrudService<T> where T : class
    {
        private InternDbContext _dbContext;
        DbSet<T> values;

        public CrudGenericRepository(InternDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public void INGetAccept(InternStatus internship, int acceptDays)
        {
            if (internship != null)
            {
                internship.AcceptStatus = true;
                internship.AcceptDay = acceptDays;
                _dbContext.SaveChanges();
            }
        }

        public T INGetById(int id)
        {
            return values.Find(id);
        }

        public void INGetConfirm(InternStatus internship)
        {
            if (internship != null)
            {
                internship.ConfirmStatus = true;
                _dbContext.SaveChanges();
            }
        }

        public void INGetContributConfirm(InternStatus internship)
        {
            if (internship != null)
            {
                internship.ContributStatus = true;
                _dbContext.SaveChanges();
            }
        }

        public void INGetContributDecline(InternStatus internship)
        {
            if (internship != null)
            {
                internship.ContributStatus = false;
                _dbContext.SaveChanges();
            }
        }

        public void INGetConfirmDecline(InternStatus internship)
        {
            if (internship != null)
            {
                internship.ConfirmStatus = false;
                _dbContext.SaveChanges();
            }
        }

        public List<T> INGetListAll()
        {
            return values.ToList();
        }

        public void INGetAcceptDecline(InternStatus internship)
        {
            if (internship != null)
            {
                internship.AcceptStatus = false;
                internship.AcceptDay = 0;
                _dbContext.SaveChanges();
            }
        }
        public void INGetReject(InternStatus internship, string rejectReason)
        {
            if (internship != null)
            {
                internship.AcceptStatus = false;
                internship.RejectReason = rejectReason;
                _dbContext.SaveChanges();
            }
        }

        public void INUpdate(T t)
        {
            values.Update(t);
            _dbContext.SaveChanges();
        }

        public int CalculateTotalInternDay(DateTime startDate, DateTime finishDate)
        {
            int totalDays = 0;
            DateTime currentDate = startDate;

            while (currentDate <= finishDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    totalDays++;
                }

                currentDate = currentDate.AddDays(1);
            }

            return totalDays;
        }
    }
}

