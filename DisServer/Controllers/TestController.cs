using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/<TestController>
        [HttpGet]
        public string Get()
        {
            try
            {
                List<TestModel> list = new()
                {
                    new TestModel { Id = 1, Name = "name1", Surname = "surname1"},
                    new TestModel { Id = 2, Name = "name2", Surname = "surname2"},
                    new TestModel { Id = 3, Name = "name3", Surname = "surname3"}
                };
                return JsonSerializer.Serialize(list);
            }
            catch
            {
                return JsonSerializer.Serialize(new ForbidResult());
            }
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            try
            {
                List<TestModel> list = new()
                {
                    new TestModel { Id = 1, Name = "name1", Surname = "surname1"},
                    new TestModel { Id = 2, Name = "name2", Surname = "surname2"},
                    new TestModel { Id = 3, Name = "name3", Surname = "surname3"}
                };
                TestModel? model = list.Where(x => x.Id == id).FirstOrDefault();
                return JsonSerializer.Serialize(model);
            }
            catch(Exception ex)
            {
                return JsonSerializer.Serialize(new ForbidResult());
            }
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Surname { get; set; }
    }
}
