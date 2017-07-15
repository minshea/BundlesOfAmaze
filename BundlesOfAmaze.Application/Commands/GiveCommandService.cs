using System;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public class GiveCommandService : IGiveCommandService
    {
        private readonly ICatRepository _repository;

        public GiveCommandService(ICatRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultMessage> HandleAsync(string ownerId, string item)
        {
            ////.amazecats feed stuff

            throw new NotImplementedException();
        }
    }
}