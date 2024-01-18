using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMq;
using StackExchange.Redis;
using UserService.Api.Model;
using UserService.Api.Services;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IRedisService _redisService;
        private readonly CrudGenericRepository<AppUser> _userGenericRepo;

        public RedisController(IRedisService redisService, CrudGenericRepository<AppUser> userGenericRepo)
        {
            _redisService = redisService;
            _userGenericRepo = userGenericRepo;
        }

        [HttpGet("SetRedisUnRegistiredStudents")]
        public async Task<IActionResult> SetRedisUnRegistiredStudents()
        {
            try
            {
                // Redis'e bağlanma komutu (varsayılan olarak 0)
                _redisService.Connection();

                // RabbitMQ bağlantısı alınıyor
                var rabbitMQConnection = RabbitMQConnection.Instance;

                // Publisher ve Consumer sınıfları oluşturuluyor
                var publisherChannel = rabbitMQConnection.CreateModel();
                var rabbitMQPublisher = new RabbitMQPublisher(publisherChannel);

                var consumerChannel = rabbitMQConnection.CreateModel();
                var rabbitMQConsumer = new RabbitMQConsumer(consumerChannel);

                // Mesaj alma
                rabbitMQConsumer.ConsumeMessages("RedisExchange", "RedisQueue", "RedisRoutingKey", async (message) =>
                {
                    var gettingMessage = message;

                    if (gettingMessage != null && gettingMessage.Equals("Sisteme kaydı Onaylanmayan öğrenciler listelendi."))
                    {
                        var Students = _userGenericRepo.UGetListAll().Where(x => x.Role == "Student").ToList();

                        foreach (var student in Students)
                        {
                            // Her öğrenci için benzersiz bir anahtar oluştur
                            var redisKey = $"Student:{student.Id}"; // Burada student.Id, öğrenci nesnesinin benzersiz kimliği olmalıdır.

                            // Eğer registerCheck true ise, Redis'teki kaydı sil
                            if (student.RegistrationCheck == true)
                            {
                                await _redisService.Keydelete(redisKey); // Redis'teki kaydı sil
                            }
                            else
                            {
                                // Sadece belirli alanları içeren bir öğrenci nesnesi oluştur
                                var redisData = new
                                {
                                    student.FirstName,
                                    student.Surname,
                                    student.Class,
                                    student.StudentNo
                                };

                                // Öğrenciyi Redis'e kaydet
                                await _redisService.StringSet(redisKey, JsonConvert.SerializeObject(redisData));
                            }
                        }
                    }
                });

                // Mesaj gönderme  
                rabbitMQPublisher.PublishMessage("RedisExchange", "RedisRoutingKey", "Sistem kaydı Onaylanmayan öğrenciler Redis'e başarıyla kaydedildi.");
                return Ok("Sistem kaydı Onaylanmayan öğrenciler Redis'e başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet("GetRedisUnRegistiredStudents")]
        public async Task<IActionResult> GetRedisUnRegistiredStudents()
        {
            try
            {
                // Redis'e bağlanma komutu (varsayılan olarak 0)
                _redisService.Connection();

                // RabbitMQ bağlantısı alınıyor
                var rabbitMQConnection = RabbitMQConnection.Instance;

                // Publisher ve Consumer sınıfları oluşturuluyor
                var publisherChannel = rabbitMQConnection.CreateModel();
                var rabbitMQPublisher = new RabbitMQPublisher(publisherChannel);

                var consumerChannel = rabbitMQConnection.CreateModel();
                var rabbitMQConsumer = new RabbitMQConsumer(consumerChannel);

                string settingMessage = null;

                // Mesaj Alma
                rabbitMQConsumer.ConsumeMessages("RedisExchange", "RedisQueue", "RedisRoutingKey", (message) =>
                {
                    var settingMessage = message;
                });

                if (settingMessage != null && settingMessage.Equals("Sistem kaydı Onaylanmayan öğrenciler Redis'e başarıyla kaydedildi."))
                {
                    var cursor = 0;
                    var unconfirmedStudents = new List<StudentRedisViewModel>();
                    do
                    {
                        var scanResult = await _redisService.Database().ExecuteAsync("SCAN", cursor.ToString(), "MATCH", "Student:*", "COUNT", "50");

                        cursor = int.Parse((string)scanResult[0]);

                        var keys = (RedisResult[])scanResult[1];

                        foreach (var key in keys)
                        {
                            var studentJson = await _redisService.StringGet(key.ToString());

                            if (!string.IsNullOrEmpty(studentJson))
                            {
                                var student = JsonConvert.DeserializeObject<StudentRedisViewModel>(studentJson);
                                unconfirmedStudents.Add(student);
                            }
                        }
                    } while (cursor != 0);

                    // Mesaj gönderme
                    rabbitMQPublisher.PublishMessage("RedisExchange", "RedisRoutingKey", "Sisteme kaydı Onaylanmayan öğrenciler listelendi.");
                    return Ok(unconfirmedStudents);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
            return Ok("Sistem kaydı Onaylanmayan öğrenciler Redisten başarıyla listelendi.");
        }

        [HttpGet("GetRedisUnConfirmedStudents")]
        public async Task<IActionResult> GetRedisUnConfirmedStudents()
        {
            try
            {
                var cursor = 0;
                var unconfirmedStudents = new List<StudentRedisViewModel>();
                do
                {
                    var userRedisResult = await _redisService.Database().ExecuteAsync("SCAN", cursor.ToString(), "MATCH", "Student:*", "COUNT", "50");
                    var confirmRedisResult = await _redisService.Database().ExecuteAsync("SCAN", cursor.ToString(), "MATCH", "Confırm:*", "COUNT", "50");

                    cursor = int.Parse((string)userRedisResult[0]);
                    var userKeys = (RedisResult[])userRedisResult[1];
                    var confirmKeys = (RedisResult[])confirmRedisResult[1];


                    foreach (var userKey in userKeys)
                    {
                        var studentJson = await _redisService.StringGet(userKey.ToString());

                        if (!string.IsNullOrEmpty(studentJson))
                        {
                            var student = JsonConvert.DeserializeObject<StudentRedisViewModel>(studentJson);

                            foreach (var confirmKey in confirmKeys)
                            {
                                var confirmKeyJson = await _redisService.StringGet(confirmKey.ToString());
                                if (!string.IsNullOrEmpty(confirmKeyJson))
                                {
                                    // Eğer Confirm key'leri içinde aynı StudentNo'ya sahip bir öğrenci varsa, listeye ekle
                                    if (student.StudentNo == confirmKeyJson)
                                    {
                                        unconfirmedStudents.Add(student);
                                    }
                                }
                            }
                        }
                    }
                }
                while (cursor != 0);
                return Ok(unconfirmedStudents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetRedisUnAcceptedStudents")]
        public async Task<IActionResult> GetRedisUnAcceptedStudents()
        {
            try
            {
                var cursor = 0;
                var unacceptedStudents = new List<StudentRedisViewModel>();

                do
                {
                    var userRedisResult = await _redisService.Database().ExecuteAsync("SCAN", cursor.ToString(), "MATCH", "Student:*", "COUNT", "50");
                    var acceptRedisResult = await _redisService.Database().ExecuteAsync("SCAN", cursor.ToString(), "MATCH", "Accept:*", "COUNT", "50");

                    cursor = int.Parse((string)userRedisResult[0]);
                    var userKeys = (RedisResult[])userRedisResult[1];
                    var acceptKeys = (RedisResult[])acceptRedisResult[1];

                    foreach (var userKey in userKeys)
                    {
                        var studentJson = await _redisService.StringGet(userKey.ToString());

                        if (!string.IsNullOrEmpty(studentJson))
                        {
                            var student = JsonConvert.DeserializeObject<StudentRedisViewModel>(studentJson);

                            foreach (var acceptKey in acceptKeys)
                            {
                                var acceptKeyJson = await _redisService.StringGet(acceptKey.ToString());
                                if (!string.IsNullOrEmpty(acceptKeyJson))
                                {
                                    // Eğer Confirm key'leri içinde aynı StudentNo'ya sahip bir öğrenci varsa, listeye ekle
                                    if (student.StudentNo == acceptKeyJson)
                                    {
                                        unacceptedStudents.Add(student);
                                    }
                                }
                            }
                        }
                    }
                } while (cursor != 0);

                return Ok(unacceptedStudents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }

}

