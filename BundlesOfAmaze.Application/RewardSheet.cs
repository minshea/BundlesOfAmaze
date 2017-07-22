using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public static class RewardSheet
    {
        public static Embed GetSheet(Cat cat)
        {
            var embedBuilder = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder() { Name = cat.Name }
            };

            return embedBuilder.Build();
        }
    }
}