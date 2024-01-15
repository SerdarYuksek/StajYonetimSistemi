using MediatR;

namespace ImageService.Api
{
    public static class ServiceRegistration
    {
        public static void AddImageService(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
