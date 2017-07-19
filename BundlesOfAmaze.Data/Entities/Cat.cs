using System;
using System.ComponentModel.DataAnnotations.Schema;
using BundlesOfAmaze.Shared;

namespace BundlesOfAmaze.Data
{
    public class Cat : Entity
    {
        public long OwnerId { get; private set; }

        public string Name { get; private set; }

        public Gender Gender { get; private set; }

        public DateTimeOffset DateOfBirth { get; private set; }

        public CatStats Stats { get; private set; }

        public string Posessive => Gender == Gender.Male ? "his" : "her";

        public string Pronoun => Gender == Gender.Male ? "he" : "she";

        [NotMapped]
        public int HungerCap { get; private set; }

        [NotMapped]
        public int ThirstCap { get; private set; }

        protected Cat()
        {
            // TODO: These caps will be modified by stats / items

            HungerCap = 14400;
            ThirstCap = 14400;
        }

        public Cat(long ownerId, string name, Gender gender)
            : this()
        {
            OwnerId = ownerId;
            Name = name;
            Gender = gender;

            DateOfBirth = DateTimeOffset.UtcNow;
            Stats = new CatStats()
            {
                Hunger = HungerCap / 2,
                Thirst = ThirstCap / 2
            };
        }

        public void Tick()
        {
            Stats.NeedsTick();
        }

        public void ApplyStatModifiers(CatStats adventureStatGain)
        {
            Stats.Hunger = (Stats.Hunger + adventureStatGain.Hunger).Clamp(0, HungerCap);
            Stats.Thirst = (Stats.Thirst + adventureStatGain.Thirst).Clamp(0, ThirstCap);
            Stats.Kind = (Stats.Kind + adventureStatGain.Kind).Clamp(0, int.MaxValue);
            Stats.Lazy = (Stats.Lazy + adventureStatGain.Lazy).Clamp(0, int.MaxValue);
            Stats.Resourceful = (Stats.Resourceful + adventureStatGain.Resourceful).Clamp(0, int.MaxValue);
            Stats.Outgoing = (Stats.Outgoing + adventureStatGain.Outgoing).Clamp(0, int.MaxValue);
            Stats.High = (Stats.High + adventureStatGain.High).Clamp(0, int.MaxValue);
        }

        public void GiveItem(Item item, int quantity)
        {
            var foodItem = item as FoodItem;
            if (foodItem != null)
            {
                if (foodItem.ItemType == ItemType.Food)
                {
                    Stats.Hunger = (Stats.Hunger + foodItem.FoodValue * quantity).Clamp(0, HungerCap);
                }
                else if (foodItem.ItemType == ItemType.Drink)
                {
                    Stats.Thirst = (Stats.Thirst + foodItem.FoodValue * quantity).Clamp(0, ThirstCap);
                }
            }
        }

        public string GetHungerLevel()
        {
            var hungerPercentage = (float)Stats.Hunger / HungerCap * 100;

            if (hungerPercentage > 95)
            {
                return "Completely stuffed!";
            }

            if (hungerPercentage > 50)
            {
                return "Full";
            }

            if (hungerPercentage > 15)
            {
                return "I'm getting hungry";
            }

            return "I'm starving";
        }

        public object GetThirstLevel()
        {
            var thirstPercentage = (float)Stats.Thirst / ThirstCap * 100;

            if (thirstPercentage > 95)
            {
                return "Completely soaked!";
            }

            if (thirstPercentage > 50)
            {
                return "Full";
            }

            if (thirstPercentage > 15)
            {
                return "Dehydrated";
            }

            return "Dry as a desert";
        }
    }
}