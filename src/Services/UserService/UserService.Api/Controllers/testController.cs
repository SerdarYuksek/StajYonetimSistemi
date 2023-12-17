using Microsoft.AspNetCore.Mvc;
using System;
using UserService.Api.Context;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public testController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult test1()
        {
            // Örnek bir SQL sorgusu
            var sqlQuery = "SELECT * FROM YourEntities WHERE SomeColumn = 'SomeValue'";

            // Sorguyu çalıştırma
            var result = _dbContext.students.ToList();

            // Sorgu sonuçlarına göre işlemler yapabilirsiniz

            return Ok(result);
        }
    }
}
