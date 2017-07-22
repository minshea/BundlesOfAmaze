using BundlesOfAmaze.Data;
using Microsoft.ApplicationInsights;

namespace BundlesOfAmaze.Application
{
    public interface ITelemetryService
    {
        TelemetryClient Client { get; }

        void TrackOverviewCommand(Owner owner, Cat cat);

        void TrackGiveCommand(Owner owner, Cat cat, Item item, int amount);

        void TrackGoCommand(Owner owner, Cat cat, Adventure adventure);

        void TrackCreateCommand(Owner owner, Cat cat);
    }
}