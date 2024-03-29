﻿using Microsoft.EntityFrameworkCore;
using UserService.Api.Context;
using UserService.Api.Model;

namespace UserService.Api.Services
{
    //User Servisindeki CRUD İşlemlerin Generic Yapı ile İnterfacesinin Yazılması
    public interface IGenericService<T>
    {
        void UAdd(T t);
        void UDelete(T t);
        void UUpdate(T t);
        List<T> UGetListAll();
        T UGetById(int id);
        void UTokenSave(AppUser appUser, string token);
        void UGetConfirm(AppUser appUser);
        void UGetConfirmDecline(AppUser appUser);
    }

    //User Servisindeki CRUD İşlemlerin Generic Yapı ile metodların Yazılması ve İnterfacenin implamente edilmesi
    public class CrudGenericRepository<T> : IGenericService<T> where T : class
    {
        private UserIdentityDbContext _dbContext;
        DbSet<T> values;

        public CrudGenericRepository(UserIdentityDbContext dbContext)
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

        public void UTokenSave(AppUser appUser, string token)
        {
            appUser.Token = token;
            _dbContext.SaveChanges();
        }

        public void UGetConfirm(AppUser appUser)
        {
            if (appUser != null)
            {
                appUser.RegistrationCheck = true;
                _dbContext.SaveChanges();
            }
        }

        public void UGetConfirmDecline(AppUser appUser)
        {
            if (appUser != null)
            {
                appUser.RegistrationCheck = false;
                _dbContext.SaveChanges();
            }
        }
    }

}
