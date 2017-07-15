using System;
using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public class CatSheet
    {
        public static Embed GetSheet(Cat cat)
        {
            var embedBuilder = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder() { Name = cat.Name }
            };

            var ageField = new EmbedFieldBuilder
            {
                IsInline = false,
                Name = "Age",
                Value = (int)((DateTimeOffset.UtcNow - cat.DateOfBirth).TotalDays / 7) + " weeks"
            };
            embedBuilder.AddField(ageField);

            var hungerField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Hunger",
                Value = cat.Stats.GetHungerLevel()
            };
            embedBuilder.AddField(hungerField);

            var thirstField = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Thirst",
                Value = cat.Stats.GetThirstLevel()
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