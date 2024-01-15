using ImageService.Api.CQRS;
using ImageService.Api.CQRS.Commands.DeleteImage;
using ImageService.Api.CQRS.Commands.SaveImage;
using ImageService.Api.CQRS.Commands.UpdateImage;
using ImageService.Api.CQRS.Queries.GetImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        //MediatR Servicesinden türetilmiş nesne
        private readonly IMediator _mediator;

        //Üretilen nesnenin ImageControllerda kullanılması için constructırda tanımlanması
        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Fotoğraf Kaydetme İşlemi
        [HttpPost("SaveImage")]
        public async Task<IActionResult> SaveImage(SaveImageCommadRequest saveImageCommadRequest)
        {
            try
            {
                SaveImageCommandResponse response = await _mediator.Send(saveImageCommadRequest);
                return Ok("Fotoğraf başarıyla kaydedildi.");
            }
            catch (PhotoNotFoundException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetImage/{Id}")]
        public async Task<IActionResult> GetImage([FromRoute] GetImageQueryRequest getImageQueryRequest)
        {
            try
            {
                GetImageQueryResponse response = await _mediator.Send(getImageQueryRequest);
                return Ok(response);
            }
            catch (PhotoNotFoundException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Fotoğraf Silme İşlemi
        [HttpDelete("DeleteImage/{Id}")]
        public async Task<IActionResult> DeleteImage([FromRoute] DeleteImageCommadRequest deleteImageCommadRequest)
        {
            try
            {
                DeleteImageCommandResponse response = await _mediator.Send(deleteImageCommadRequest);
                return Ok("Fotoğraf başarıyla silindi.");
            }
            catch (PhotoNotFoundException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Fotoğraf Güncelleme İşlemi
        [HttpPost("UpdateImage{Id}")]
        public async Task<IActionResult> UpdateImage([FromRoute] UpdateImageCommadRequest updateImageCommadRequest)
        {
            try
            {
                UpdateImageCommandResponse response = await _mediator.Send(updateImageCommadRequest);
                return Ok("Fotoğraf başarıyla güncellendi ve kaydedildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
