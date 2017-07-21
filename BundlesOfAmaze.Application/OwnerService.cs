using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public class OwnerService : IOwnerService, ICurrentOwner
    {
        public Owner Owner { get; private set; }

        public void SetCurrentOwner(Owner owner)
        {
            Owner = owner;
        }
    }
}