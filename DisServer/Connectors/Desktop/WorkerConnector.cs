using DatabaseController;
using DatabaseController.Models;
using DisServer.Models.Desktop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisServer.Connectors.Desktop
{
    public class WorkerConnector
    {
        public async Task<bool> CheckAdminAsync()
        {
            try
            {
                using DataContext context = new();

                var counAdmin = await context.Workers.Where(w => w.isAdmin).CountAsync();

                if (counAdmin > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int?> CheckWorkerAsync(string login, string password)
        {
            try
            {
                using DataContext context = new();

                var worker = await context.Workers.Where(w => w.Login == login && w.Password == password).FirstOrDefaultAsync();

                if (password == null)
                    return null;
                else
                    return worker.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Worker>> GetWorkersAsync()
        {
            try
            {
                using DataContext context = new();

                var workers = await context.Workers.ToListAsync();

                return workers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Worker> GetWorkerByIdAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var worker = await context.Workers.Where(w => w.Id == id).FirstOrDefaultAsync();

                return worker ?? new();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> PostWorkerAsync(WorkerModel model)
        {
            try
            {
                using DataContext context = new();

                var worker = await context.Workers.Where(w => w.Login == model.Login).FirstOrDefaultAsync();

                if (worker != null)
                    throw new Exception();

                var newWorker = new Worker()
                {
                    Login = model.Login,
                    Password = model.Password,
                    FullName = model.FullName,
                    isAdmin = model.IsAdmin,
                };

                context.Workers.Add(newWorker);

                await context.SaveChangesAsync();

                return newWorker.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task PutWorkerAsync(WorkerModel model)
        {
            try
            {
                using DataContext context = new();

                var worker = await context.Workers.Where(w => w.Id == model.Id).FirstOrDefaultAsync();

                if (worker?.Login != model.Login)
                {
                    var checWorker = await context.Workers.Where(w => w.Login == model.Login).FirstOrDefaultAsync();
                    if(checWorker != null)
                        throw new Exception();
                }

                if (worker == null)
                    throw new Exception();

                worker.Login = model.Login;
                worker.Password = model.Password;
                worker.FullName = model.FullName;
                worker.isAdmin = model.IsAdmin;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteWorkerAsync(int id)
        {
            try
            {
                using DataContext context = new();

                var worker = await context.Workers.Where(w => w.Id == id).FirstOrDefaultAsync();

                if (worker == null)
                    return;

                context.Workers.Remove(worker);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
