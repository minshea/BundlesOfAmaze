namespace BundlesOfAmaze.Data
{
    public class Stats : Entity
    {
        public long CatId { get; set; }

        public int Hunger { get; set; }

        public virtual Cat Cat { get; private set; }

        protected Stats()
        {
        }

        public Stats(int hunger)
            : this()
        {
            Hunger = hunger;
        }

        public void SetHunger(int hunger)
        {
            Hunger = hunger;
        }

        public string GetHungerLevel()
        {
            if (Hunger > 95)
            {
                return "Completely stuffed!";
            }

            if (Hunger > 50)
            {
                return "Full";
            }

            if (Hunger > 15)
            {
                return "Hungry";
            }

            return "Starving";
        }
    }
}