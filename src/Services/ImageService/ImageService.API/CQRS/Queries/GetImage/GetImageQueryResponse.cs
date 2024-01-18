namespace ImageService.Api.CQRS.Queries.GetImage
{
    public class GetImageQueryResponse
    {
        public byte[] Bytes { get; set; }
        public string ImageName { get; set; }
        public string? Error { get; set; }
    }
}
