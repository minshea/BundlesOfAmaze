namespace BundlesOfAmaze.Data
{
    public interface IAdventureRepository
    {
        Adventure FindByAdventureRef(AdventureRef adventureRef);
    }
}