using System;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.Shared;
using Discord;
using Discord.Commands;

namespace BundlesOfAmaze.Application
{
    [Name("Create")]
    public class CreateModule : ModuleBase
    {
        private readonly ICurrentOwner _currentOwner;
        private readonly ICatRepository _repository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ITelemetryService _telemetryService;

        public CreateModule(ICurrentOwner currentOwner, ICatRepository repository, IOwnerRepository ownerRepository, ITelemetryService telemetryService)
        {
            _currentOwner = currentOwner;
            _repository = repository;
            _ownerRepository = ownerRepository;
            _telemetryService = telemetryService;
        }

        [Command("create")]
        [Summary("Creates a new cat. Use 'help create' for more information")]
        [Remarks("Usage: create [name] [gender:male|female]\nCommand to create a new cat\nex. create Kitty female")]
        public async Task HandleAsync(string rawName, string rawGender)
        {
            ////.amazecats create Name Male

            if (string.IsNullOrWhiteSpace(rawName) || string.IsNullOrWhiteSpace(rawGender))
            {
                await ReplyAsync(Messages.InvalidCommand);
                return;
            }

            var name = rawName.Trim();

            // Check if the cat already exists
            var existingCat = await _repository.FindByNameAsync(name);
            if (existingCat != null)
            {
                await ReplyAsync($"Error: A cat with the name '{name}' already exists");
                return;
            }

            if (!Enum.TryParse(rawGender.Trim(), true, out Gender gender) || gender == Gender.None)
            {
                await ReplyAsync($"Error: Valid genders are '{Gender.Male}' and '{Gender.Female}'");
                return;
            }

            // If there is no owner yet, generate one
            var owner = _currentOwner.Owner ?? await CreateOwnerAsync(Context.Message.Author);

            // Generate the new cat
            var newCat = new Cat(owner.Id, name, gender);

            // Store the new cat
            _repository.Add(newCat);
            await _repository.SaveChangesAsync();

            // Track event
            _telemetryService.TrackCreateCommand(_currentOwner.Owner, newCat);

            var catSheet = CatSheet.GetSheet(newCat);
            var message = $"{newCat.Name} happily meets {newCat.Posessive} new master\n";

            await ReplyAsync(message, embed: catSheet);
        }

        private async Task<Owner> CreateOwnerAsync(IUser user)
        {
            var owner = new Owner(user.Id.ToString(), user.Username);

            // Give some initial items for the new owner to start out with
            owner.GiveItem(ItemRef.Water, 10);
            owner.GiveItem(ItemRef.Tuna, 10);

            _ownerRepository.Add(owner);
            await _ownerRepository.SaveChangesAsync();

            return owner;
        }
    }
}