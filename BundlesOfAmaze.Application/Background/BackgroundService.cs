using System;
using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public class BackgroundService : IBackgroundService
    {
        private readonly ICatRepository _repository;

        public BackgroundService(ICatRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleTickAsync()
        {
            Console.WriteLine("Cron tick!");
            var cats = await _repository.FindAllAsync();
            if (cats.Any())
            {
                foreach (var cat in cats)
                {
                    cat.Tick();
                }

                await _repository.SaveChangesAsync();
            }
        }
    }
}