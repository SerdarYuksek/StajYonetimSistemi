namespace Application.Interfaces
{
    // Domain katmanındaki entitylerin crud işlemlerinin generic haldeki interfacesi
    public interface IGenericCrudInterface<T>
    {
        void SAdd(T t);
        void SDelete(T t);
        void SUpdate(T t);
        List<T> SGetListAll();
        T SGetById(int? id);
    }
}
