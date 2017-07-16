using System;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public class CreateCommandService : ICreateCommandService
    {
        private readonly ICatRepository _repository;

        public CreateCommandService(ICatRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultMessage> HandleAsync(long ownerId, string rawName, string rawGender)
        {
            ////.amazecats create Name Male

            if (string.IsNullOrWhiteSpace(rawName) || string.IsNullOrWhiteSpace(rawGender))
            {
                return new ResultMessage($"Error: Invalid command");
            }

            var name = rawName.Trim();

            // Check if the cat already exists
            var existingCat = await _repository.FindByNameAsync(name);
            if (existingCat != null)
            {
                return new ResultMessage($"Error: A cat with the name '{name}' already exists");
            }

            Gender gender;
            if (!Enum.TryParse(rawGender.Trim(), true, out gender) || gender == Gender.None)
            {
                return new ResultMessage($"Error: Valid genders are '{Gender.Male}' and '{Gender.Female}'");
            }

            // Generate the new cat
            var newCat = new Cat(ownerId, name, gender);

            // Store the new cat
            await _repository.AddAsync(newCat);
            await _repository.SaveChangesAsync();

            var catSheet = CatSheet.GetSheet(newCat);
            var result = $"{newCat.Name} happily meets {newCat.Addressing} new master\n";

            return new ResultMessage(result, catSheet);
        }
    }
}