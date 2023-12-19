namespace SurveyService.Api.Services
{
    //Survey servisindeki CRUD İşlemlerin Generic Yapı ile İnterfacesinin Yazılması
    public interface IGenericCrudInterface<T>
    {

        void SAdd(T t);
        void SDelete(T t);
        void SUpdate(T t);
        List<T> SGetListAll();
        T SGetById(int id);

    }
}
