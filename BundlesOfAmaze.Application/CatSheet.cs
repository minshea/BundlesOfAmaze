using System;
using System.Globalization;
using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public static class CatSheet
    {
        public static Embed GetSheet(Cat cat, string adventure = null, DateTimeOffset? adventureEnd = null)
        {
            var embedBuilder = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder() { Name = cat.Name }
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
                var remaining = adventureEnd.Value - DateTimeOffset.UtcNow;

                var adventureField = new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = "Current adventure",
                    Value = $"{adventure} - {remaining.ToString("HH:mm:ss", CultureInfo.InvariantCulture)} remaining"
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
    }
}