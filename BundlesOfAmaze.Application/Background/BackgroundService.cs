using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord;
using Discord.WebSocket;

namespace BundlesOfAmaze.Application
{
    public class BackgroundService : IBackgroundService
    {
        private readonly IDiscordClient _discordClient;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICatRepository _catRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IAdventureEntryRepository _adventureEntryRepository;
        private readonly IAdventureRepository _adventureRepository;

        public BackgroundService(
            IDiscordClient discordClient,
            IOwnerRepository ownerRepository,
            ICatRepository catRepository,
            IItemRepository itemRepository,
            IAdventureEntryRepository adventureEntryRepository,
            IAdventureRepository adventureRepository)
        {
            _discordClient = discordClient;
            _ownerRepository = ownerRepository;
            _catRepository = catRepository;
            _itemRepository = itemRepository;
            _adventureEntryRepository = adventureEntryRepository;
            _adventureRepository = adventureRepository;
        }

        public async Task HandleTickAsync()
        {
            Console.WriteLine("Cron tick!");

            var cats = await _catRepository.FindAllAsync();

            // Handle cat needs tick
            HandleCats(cats);

            // Handle adventures
            await HandleAdventuresAsync(cats);

            // Commit all changes
            await _catRepository.SaveChangesAsync();
        }

        private void HandleCats(IEnumerable<Cat> cats)
        {
            foreach (var cat in cats)
            {
                Console.WriteLine($"Handle {cat.Name}");
                cat.Tick();
            }
        }

        private async Task HandleAdventuresAsync(IEnumerable<Cat> cats)
        {
            var now = DateTimeOffset.UtcNow;
            var adventuresInProgress = await _adventureEntryRepository.FindAllAsync();
            foreach (var adventureEntry in adventuresInProgress)
            {
                if (adventureEntry.End <= now)
                {
                    Console.WriteLine($"Removing expired adventure {adventureEntry.AdventureRef} for cat {adventureEntry.CatId}");
                    _adventureEntryRepository.Remove(adventureEntry);

                    var cat = cats.First(i => i.Id == adventureEntry.CatId);

                    // Handle adventure rewards
                    await HandleAdventureResultAsync(cat, adventureEntry);
                }
            }
        }

        private async Task HandleAdventureResultAsync(Cat cat, AdventureEntry adventureEntry)
        {
            var adventure = _adventureRepository.FindByAdventureRef(adventureEntry.AdventureRef);

            var owner = await _ownerRepository.FindAsync(cat.OwnerId);
            var reward = adventure.GetReward();
            var item = await _itemRepository.FindByItemRefAsync(reward.ItemRef);

            cat.ApplyStatModifiers(adventure.StatGain);
            owner.GiveItem(reward.ItemRef, reward.Quantity);

            var user = await _discordClient.GetUserAsync(Convert.ToUInt64(owner.AuthorId));
            var guild = ((DiscordSocketClient)_discordClient).Guilds.FirstOrDefault(i => i.Users.Any(j => j.Id == user.Id));

            // TODO: For now we select the default channel. Replace with configurable channel
            var channel = guild.GetTextChannel(guild.DefaultChannel.Id);

            await channel.SendMessageAsync($"{cat.Name} got {reward.Quantity} {item.Name}! (user)");
        }
    }
}