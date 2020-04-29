using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweight_Pattern
{
    // Flyweight blueprint
    public interface IDrinkFlyweight
    {
        // Intrinsic state - shared/readonly
        string Name { get; }

        // Extrinsic state
        void Serve(string size);
    }

    public class Espresso: IDrinkFlyweight
    {
        private string _name;
        public string Name { get { return _name; } }
        private IEnumerable<string> _ingredients;

        public Espresso()
        {
            _name = "Espresso";
            _ingredients = new List<string>()
            {
                "Coffee Beans", 
                "Hot Water"
            };
        }

        public void Serve(string size)
        {
            Console.WriteLine($"- {size} {_name} with {string.Join(", ", _ingredients)} coming up!");
        }
    }

    public class BananaSmoothie : IDrinkFlyweight
    {
        private string _name;
        public string Name { get { return _name; } }
        private IEnumerable<string> _ingredients;

        public BananaSmoothie()
        {
            _name = "Banana Smoothie";
            _ingredients = new List<string>()
            {
                "Banana",
                "Whole Milk",
                "Vanilla Extract"
            };
        }

        public void Serve(string size)
        {
            Console.WriteLine($"- {size} {_name} with {string.Join(", ", _ingredients)} coming up!");
        }
    }

    // Unshared concrete flyweight
    public class DrinkGiveaway: IDrinkFlyweight
    {
        // All state
        public string Name { get { return _randomDrink.Name; } }
        private IDrinkFlyweight[] _eligibleDrinks = new IDrinkFlyweight[]
        {
            new Espresso(),
            new BananaSmoothie()
        };

        private IDrinkFlyweight _randomDrink;
        private string _size;

        public DrinkGiveaway()
        {
            var randomIndex = new Random().Next(0, 2);
            _randomDrink = _eligibleDrinks[randomIndex];
        }

        // Extrinsic state
        public void Serve(string size)
        {
            _size = size;
            Console.WriteLine($"Free Giveaway!");
            Console.WriteLine($"- {_size} {_randomDrink.Name} coming up!");
        }
    }
}
