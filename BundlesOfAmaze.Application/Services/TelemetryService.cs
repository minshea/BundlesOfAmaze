using System.Collections.Generic;
using BundlesOfAmaze.Data;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace BundlesOfAmaze.Application
{
    public class TelemetryService : ITelemetryService
    {
        public TelemetryClient Client { get; private set; }

        public TelemetryService()
        {
            Client = new TelemetryClient(TelemetryConfiguration.Active);
        }

        public void TrackOverviewCommand(ICurrentOwner owner, Cat cat)
        {
            var properties = new Dictionary<string, string>()
            {
                { "ownerId", owner.Owner.Id.ToString() },
                { "ownerName", owner.Owner.Name },
                { "catId", cat.Id.ToString() },
                { "catName", cat.Name }
            };

            Client.TrackEvent("overview", properties);
            Client.Flush();
        }

        public void TrackGiveCommand(ICurrentOwner owner, Cat cat, Item item, int amount)
        {
            var properties = new Dictionary<string, string>()
            {
                { "ownerId", owner.Owner.Id.ToString() },
                { "ownerName", owner.Owner.Name},
                { "catId", cat.Id.ToString() },
                { "catName", cat.Name },
                { "itemRef", item.ItemRef.ToString() },
                { "itemType", item.ItemType.ToString() }
            };

            var metrics = new Dictionary<string, double>()
            {
                { "amount", amount }
            };

            Client.TrackEvent("give", properties, metrics);
            Client.Flush();
        }

        public void TrackGoCommand(ICurrentOwner owner, Cat cat, Adventure adventure)
        {
            var properties = new Dictionary<string, string>()
            {
                { "ownerId", owner.Owner.Id.ToString() },
                { "ownerName", owner.Owner.Name },
                { "catId", cat.Id.ToString() },
                { "catName", cat.Name },
                { "adventureRef", adventure.AdventureRef.ToString() }
            };

            Client.TrackEvent("go", properties);
            Client.Flush();
        }

        public void TrackCreateCommand(ICurrentOwner owner, Cat cat)
        {
            var properties = new Dictionary<string, string>()
            {
                { "ownerId", owner.Owner.Id.ToString() },
                { "ownerName", owner.Owner.Name },
                { "catId", cat.Id.ToString() },
                { "catName", cat.Name }
            };

            Client.TrackEvent("create", properties);
            Client.Flush();
        }
    }
}