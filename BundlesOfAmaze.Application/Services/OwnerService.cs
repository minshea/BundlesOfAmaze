using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public class OwnerService : IOwnerService, ICurrentOwner
    {
        public IUser User { get; private set; }
        public Owner Owner { get; private set; }
        public Cat Cat { get; private set; }

        private readonly IOwnerRepository _ownerRepository;
        private readonly ICatRepository _catRepository;

        public OwnerService(IOwnerRepository ownerRepository, ICatRepository catRepository)
        {
            _ownerRepository = ownerRepository;
            _catRepository = catRepository;
        }

        public async Task Initialize(IUser user)
        {
            var authorId = user.Id.ToString();

            User = user;
            Owner = await _ownerRepository.FindByAuthorIdAsync(authorId);
            Cat = await GetCatAsync();

            await UpdateOwnerAsync(user);
        }

        private async Task UpdateOwnerAsync(IUser user)
        {
            // Update the owner if any requuired data changed
            if (Owner != null && (Owner.Name != user.Username || Owner.AvatarUrl != user.GetAvatarUrl(ImageFormat.Auto, 64)))
            {
                Owner.Update(user);
                await _ownerRepository.SaveChangesAsync();
            }
        }

        public async Task SetCurrentOwner(Owner owner)
        {
            Owner = owner;
            Cat = await GetCatAsync();
        }

        private async Task<Cat> GetCatAsync()
        {
            if (Owner != null)
            {
                return await _catRepository.FindByOwnerAsync(Owner.Id);
            }

            return null;
        }
    }
}