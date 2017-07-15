namespace BundlesOfAmaze.Data
{
    public class Stats : Entity
    {
        public long CatId { get; set; }

        public int Hunger { get; set; }

        public int Thirst { get; set; }

        public int Kind { get; set; }

        public int Lazy { get; set; }

        public int Resourceful { get; set; }

        public int Outgoing { get; set; }

        public int High { get; set; }

        public virtual Cat Cat { get; private set; }

        protected Stats()
        {
        }

        public Stats(int hunger, int thirst)
            : this()
        {
            Hunger = hunger;
            Thirst = thirst;
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
                return "I'm getting hungry";
            }

            return "I'm starving";
        }

        public object GetThirstLevel()
        {
            if (Thirst > 95)
            {
                return "Completely soaked!";
            }

            if (Thirst > 50)
            {
                return "Full";
            }

            if (Thirst > 15)
            {
                return "Dehydrated";
            }

            return "Dry as a desert";
        }
    }
}