using System;

namespace BundlesOfAmaze.Data
{
    public class Cat : Entity
    {
        public string OwnerId { get; private set; }

        public string Name { get; private set; }

        public Gender Gender { get; private set; }

        public DateTimeOffset DateOfBirth { get; private set; }

        public Stats Stats { get; private set; }

        public string Addressing => Gender == Gender.Male ? "his" : "her";

        protected Cat()
        {
        }

        public Cat(string ownerId, string name, Gender gender)
            : this()
        {
            OwnerId = ownerId;
            Name = name;
            Gender = gender;

            DateOfBirth = DateTimeOffset.UtcNow;
            Stats = new Stats(50, 50);
        }

        public void Tick()
        {
            Console.WriteLine($"Handle {Name}");
        }
    }
}