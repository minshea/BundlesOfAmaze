using System;

namespace BundlesOfAmaze.Data
{
    public class Stats : Entity
    {
        #region EF Mapping

        public long CatId { get; set; }

        public virtual Cat Cat { get; private set; }

        #endregion EF Mapping

        public int Hunger { get; set; }

        public int Thirst { get; set; }

        public int Kind { get; set; }

        public int Lazy { get; set; }

        public int Resourceful { get; set; }

        public int Outgoing { get; set; }

        public int High { get; set; }

        protected Stats()
        {
        }

        public Stats(int hunger, int thirst)
            : this()
        {
            Hunger = hunger;
            Thirst = thirst;
        }

        public void NeedsTick()
        {
            Hunger -= 20;
            Hunger = Hunger < 0 ? 0 : Hunger;
            Console.WriteLine($"Hunger: {Hunger}");

            Thirst -= 10;
            Thirst = Thirst < 0 ? 0 : Thirst;
            Console.WriteLine($"Thirst: {Thirst}");
        }
    }
}