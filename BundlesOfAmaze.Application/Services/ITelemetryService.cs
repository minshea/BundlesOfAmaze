using BundlesOfAmaze.Data;
using Microsoft.ApplicationInsights;

namespace BundlesOfAmaze.Application
{
    public interface ITelemetryService
    {
        TelemetryClient Client { get; }

        void TrackOverviewCommand(ICurrentOwner owner, Cat cat);

        void TrackGiveCommand(ICurrentOwner owner, Cat cat, Item item, int amount);

        void TrackGoCommand(ICurrentOwner owner, Cat cat, Adventure adventure);

        void TrackCreateCommand(ICurrentOwner owner, Cat cat);
    }
}