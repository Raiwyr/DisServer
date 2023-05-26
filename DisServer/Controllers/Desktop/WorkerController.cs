using DatabaseController.Models;
using DisServer.Connectors.Desktop;
using DisServer.Models.Desktop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DisServer.Controllers.Desktop
{
    [Route("api/desktop/worker")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerConnector connector;

        public WorkerController()
        {
            connector = new();
        }

        [HttpGet("check/admin")]
        public async Task<object> CheckAdminAsync()
        {
            try
            {
                return await connector.CheckAdminAsync();
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        [HttpGet("check/user")]
        public async Task<object> CheckWorkerAsync(string login, string pass)
        {
            try
            {
                return await connector.CheckWorkerAsync(login, pass);
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<object> GetWorkersAsync()
        {
            try
            {
                var workers = await connector.GetWorkersAsync();

                var wokerHeaderModels = workers.Select(w => new WorkerModel()
                {
                    Id = w.Id,
                    FullName = w.FullName,
                    Login = w.Login,
                    Password = "",
                    IsAdmin = w.isAdmin

                }).ToList();

                var response = JsonConvert.SerializeObject(wokerHeaderModels);

                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<object> GetWorkerByIdAsync(int id)
        {
            try
            {
                var worker = await connector.GetWorkerByIdAsync(id);

                var wokerModel = new WorkerModel()
                {
                    Id = worker.Id,
                    Login = worker.Login,
                    Password = worker.Password,
                    FullName = worker.FullName,
                    IsAdmin = worker.isAdmin
                };

                var response = JsonConvert.SerializeObject(wokerModel);

                return response;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<object> PostWorkerAsync([FromBody] string model)
        {
            try
            {
                var worker = JsonConvert.DeserializeObject<WorkerModel>(model);

                if (worker == null)
                    throw new Exception();

                int id = await connector.PostWorkerAsync(worker);

                return id;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<object> PutWorkerAsync([FromBody] string model)
        {
            try
            {
                var worker = JsonConvert.DeserializeObject<WorkerModel>(model);

                if (worker == null)
                    throw new Exception();

                await connector.PutWorkerAsync(worker);

                return true;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<object> DeleteWorkerAsync(int id)
        {
            try
            {
                await connector.DeleteWorkerAsync(id);

                return true;
            }
            catch (Exception ex)
            {
                return new ForbidResult();
            }
        }
    }
}
