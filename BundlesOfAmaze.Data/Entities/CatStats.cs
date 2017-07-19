using System;

namespace BundlesOfAmaze.Data
{
    public class CatStats : Entity
    {
        #region EF Mapping

        public long CatId { get; private set; }

        public virtual Cat Cat { get; private set; }

        #endregion EF Mapping

        public int Hunger { get; set; }

        public int Thirst { get; set; }

        public int Kind { get; set; }

        public int Lazy { get; set; }

        public int Resourceful { get; set; }

        public int Outgoing { get; set; }

        public int High { get; set; }

        public CatStats()
        {
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