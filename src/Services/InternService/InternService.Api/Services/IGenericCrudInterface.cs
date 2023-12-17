namespace InternService.Api.Services
{
    public interface IGenericCrudInterface
    {
        public interface IGenericService<T>
        {
            void INAdd(T t);
            void INDelete(T t);
            void INUpdate(T t);
            List<T> INGetListAll();
            void INGetConfirm(T t);
            void INGetAccept(T t);
            void INGetDecline(T t);
            void INGetReject(T t);
            void INGetContributConfirm(T t);
            void INGetContributDecline(T t);
            T INGetById(int id);
        }
    }
}
