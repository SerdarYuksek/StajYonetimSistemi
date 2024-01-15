namespace ImageService.Api.CQRS
{
    public class PhotoNotFoundException : Exception
    {
        public PhotoNotFoundException(string message) : base(message)
        {
        }
    }
}