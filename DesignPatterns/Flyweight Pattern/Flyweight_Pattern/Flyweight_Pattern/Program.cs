using System;

namespace Flyweight_Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var drinkFactory = new DrinkFactory();

            /*var largeEspresso = drinkFactory.GetDrink("Espresso");
            largeEspresso.Serve("Large");

            var mediumSmoothie = drinkFactory.GetDrink("BananaSmoothie");
            mediumSmoothie.Serve("Medium");

            var smallEspresso = drinkFactory.GetDrink("Espresso");
            smallEspresso.Serve("Small");

            drinkFactory.ListDrinks();*/

            var sizes = new string[] { "Small", "Medium", "Large" };
            foreach(var size in sizes)
            {
                var giveaway = drinkFactory.CreateGiveaway();
                giveaway.Serve(size);
            }
        }
    }
}