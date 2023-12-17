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
        }
    
}
