using System;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.Shared;
using Discord.WebSocket;

namespace BundlesOfAmaze.Application
{
    public class CreateCommandService : ICreateCommandService
    {
        private readonly ICatRepository _repository;
        private readonly IOwnerRepository _ownerRepository;

        public CreateCommandService(ICatRepository repository, IOwnerRepository ownerRepository)
        {
            _repository = repository;
            _ownerRepository = ownerRepository;
        }

        public async Task<ResultMessage> HandleAsync(Owner owner, string rawName, string rawGender)
        {
            ////.amazecats create Name Male

            if (string.IsNullOrWhiteSpace(rawName) || string.IsNullOrWhiteSpace(rawGender))
            {
                return new ResultMessage(Messages.InvalidCommand);
            }

            var name = rawName.Trim();

            // Check if the cat already exists
            var existingCat = await _repository.FindByNameAsync(name);
            if (existingCat != null)
            {
                return new ResultMessage($"Error: A cat with the name '{name}' already exists");
            }

            if (!Enum.TryParse(rawGender.Trim(), true, out Gender gender) || gender == Gender.None)
            {
                return new ResultMessage($"Error: Valid genders are '{Gender.Male}' and '{Gender.Female}'");
            }

            // Generate the new cat
            var newCat = new Cat(owner.Id, name, gender);

            // Store the new cat
            _repository.Add(newCat);
            await _repository.SaveChangesAsync();

            var catSheet = CatSheet.GetSheet(newCat);
            var result = $"{newCat.Name} happily meets {newCat.Posessive} new master\n";

            return new ResultMessage(result, catSheet);
        }

        public async Task<Owner> CreateOwnerAsync(SocketUserMessage msg)
        {
            var owner = new Owner(msg.Author.Id.ToString(), msg.Author.Username);

            // Give some initial items for the new owner to start out with
            owner.GiveItem(ItemRef.Water, 10);
            owner.GiveItem(ItemRef.Tuna, 10);

            _ownerRepository.Add(owner);
            await _ownerRepository.SaveChangesAsync();

            return owner;
        }
    }
}