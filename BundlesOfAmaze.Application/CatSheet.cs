using System;
using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public static class CatSheet
    {
        public static Embed GetSheet(ICurrentOwner currentOwner, Cat cat, string message = null, Adventure adventure = null, DateTimeOffset? adventureEnd = null)
        {
            var embedBuilder = new EmbedBuilder
            {
                Color = new Color(50, 184, 31),
                Author = new EmbedAuthorBuilder
                {
                    Name = $"{currentOwner.Owner.Name}'s {cat.Name}",
                    IconUrl = currentOwner.Owner.AvatarUrl
                },
                Description = message
            };

            var ageField = new EmbedFieldBuilder
            {
                IsInline = adventure != null,
                Name = "Age",
                Value = (int)((DateTimeOffset.UtcNow - cat.DateOfBirth).TotalDays / 7) + " weeks"
            };
            embedBuilder.AddField(ageField);

            if (adventure != null && adventureEnd.HasValue)
            {
                var remaining = "completed";
                var remainingTime = adventureEnd.Value - DateTimeOffset.UtcNow;
                if (remainingTime.TotalMilliseconds > 0)
                {
                    remaining = remainingTime.ToString("hh:mm:ss");
                }

                var adventureField = new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = "Current adventure",
                    Value = $"{adventure.Name} - {remaining} remaining"
                };
                embedBuilder.AddField(adventureField);
            }

            var hungerField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Hunger",
                Value = cat.GetHungerLevel()
            };
            embedBuilder.AddField(hungerField);

            var thirstField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Thirst",
                Value = cat.GetThirstLevel()
            };
            embedBuilder.AddField(thirstField);

            var kindField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Kindness",
                Value = cat.Stats.Kind
            };
            embedBuilder.AddField(kindField);

            var lazyField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Laziness",
                Value = cat.Stats.Lazy
            };
            embedBuilder.AddField(lazyField);

            var resourcefulField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Resourcefulness",
                Value = cat.Stats.Resourceful
            };
            embedBuilder.AddField(resourcefulField);

            var outgoingField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Outgoing",
                Value = cat.Stats.Outgoing
            };
            embedBuilder.AddField(outgoingField);

            return embedBuilder.Build();
        }

        public static Embed GetRewardSheet(Owner owner, Cat cat, Adventure adventure, Item item, DropPoolItem reward)
        {
            var embedBuilder = new EmbedBuilder
            {
                Color = new Color(226, 193, 5),
                Author = new EmbedAuthorBuilder
                {
                    Name = $"{owner.Name}'s {cat.Name}",
                    IconUrl = owner.AvatarUrl
                },
                Description = $"{cat.Name} has returned from {adventure.Name} and brought:"
            };

            var itemField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = $"{reward.Quantity}x {item.Name}",
                Value = item.Description
            };
            embedBuilder.AddField(itemField);

            return embedBuilder.Build();
        }
    }
}