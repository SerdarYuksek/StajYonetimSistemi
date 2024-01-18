using MediatR;

namespace ImageService.Api.CQRS.Commands.SaveImage
{
    public class SaveImageCommadRequest : IRequest<SaveImageCommandResponse>
    {
        public IFormFile File { get; set; }

        public string UserNo { get; set; }
    }
}
