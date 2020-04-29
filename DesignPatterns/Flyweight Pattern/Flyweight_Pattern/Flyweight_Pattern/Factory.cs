using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweight_Pattern
{
    public class DrinkFactory
    {
        private Dictionary<string, IDrinkFlyweight> _drinkCache = new Dictionary<string, IDrinkFlyweight>();
        public int ObjectsCreated = 0;

        public IDrinkFlyweight GetDrink(string drinkKey)
        {
            IDrinkFlyweight drink = null;

            if(_drinkCache.ContainsKey(drinkKey))
            {
                Console.WriteLine("\nReusing existing flyweight object.");
                return _drinkCache[drinkKey];
            }
            else
            {
                Console.WriteLine("\nCreating new flyweight object.");
                switch(drinkKey)
                {
                    case "Espresso":
                        drink = new Espresso();
                        break;
                    case "BananaSmoothie":
                        drink = new BananaSmoothie();
                        break;
                    default:
                        throw new Exception("This is not a flyweight drink object...");
                }
            }

            _drinkCache.Add(drinkKey, drink);
            ObjectsCreated++;

            return drink;
        }

        public IDrinkFlyweight CreateGiveaway()
        {
            return new DrinkGiveaway();
        }

        public void ListDrinks()
        {
            Console.WriteLine($"\nFactory has {_drinkCache.Count} drink objects ready to use.");
            Console.WriteLine($"Number of objects created: {ObjectsCreated}");

            foreach (var drink in _drinkCache)
                Console.WriteLine($"\t{drink.Value.Name}");

            Console.WriteLine("\n");
        }
    }
}
