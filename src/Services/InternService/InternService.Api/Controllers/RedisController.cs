using InternService.Api.Model;
using InternService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : Controller
    {
        private readonly IRedisService _redisService;
        private readonly CrudGenericRepository<InternInfo> _internInfosGenericRepo;
        private readonly CrudGenericRepository<InternStatus> _internStatusGenericRepo;

        public RedisController(IRedisService redisService, CrudGenericRepository<InternInfo> internInfosGenericRepo, CrudGenericRepository<InternStatus> internStatusGenericRepo)
        {
            _redisService = redisService;
            _internInfosGenericRepo = internInfosGenericRepo;
            _internStatusGenericRepo = internStatusGenericRepo;
        }

        [HttpGet("SetRedisUnConfirmedStudents")]
        public async Task<IActionResult> SetRedisUnConfirmedStudents()
        {
            // Redis'e bağlanma komutu (varsayılan olarak 0)
            _redisService.Connection();

            var internInfos = _internInfosGenericRepo.INGetListAll().ToList();
            var internStatus = _internStatusGenericRepo.INGetListAll().ToList();

            foreach (var interns in internInfos)
            {
                // Her öğrenci için benzersiz bir anahtar oluştur
                var redisKey = $"Confırm:{interns.ID}"; // Burada student.Id, öğrenci nesnesinin benzersiz kimliği olmalıdır.

                // Eğer registerCheck true ise, Redis'teki kaydı sil
                if (internStatus.Any(status => status.ConfirmStatus == true))
                {
                    await _redisService.Keydelete(redisKey); // Redis'teki kaydı sil
                }
                else
                {
                    // Sadece belirli alanları içeren bir öğrenci nesnesi oluştur
                    var redisData = interns.StudentNo;

                    // Öğrenciyi Redis'e kaydet
                    await _redisService.StringSet(redisKey, redisData);
                }
            }
            return Ok("Staj kaydı Onaylanmayan öğrenciler Redis'e başarıyla kaydedildi.");
        }


        [HttpGet("SetRedisUnAcceptedStudents")]
        public async Task<IActionResult> SetRedisUnAcceptedStudents()
        {
            // Redis'e bağlanma komutu (varsayılan olarak 0)
            _redisService.Connection();

            var internInfos = _internInfosGenericRepo.INGetListAll().ToList();
            var internStatus = _internStatusGenericRepo.INGetListAll().ToList();

            foreach (var interns in internInfos)
            {
                // Her öğrenci için benzersiz bir anahtar oluştur
                var redisKey = $"Accept:{interns.ID}"; // Burada student.Id, öğrenci nesnesinin benzersiz kimliği olmalıdır.

                // Eğer registerCheck true ise, Redis'teki kaydı sil
                if (internStatus.Any(status => status.AcceptStatus == true))
                {
                    await _redisService.Keydelete(redisKey); // Redis'teki kaydı sil
                }
                else
                {
                    // Sadece belirli alanları içeren bir öğrenci nesnesi oluştur
                    var redisData = interns.StudentNo;

                    // Öğrenciyi Redis'e kaydet
                    await _redisService.StringSet(redisKey, redisData);
                }
            }
            return Ok("Staj kaydı Kabul edilmeyen öğrenciler Redis'e başarıyla kaydedildi.");
        }
    }
}
