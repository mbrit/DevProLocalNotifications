using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifier
{
    public class Barista
    {
        internal DeliciousDrink BrewMeAnything()
        {
            var rand = new Random();

            // create...
            var drink = new DeliciousDrink();
            drink.NumShots = rand.Next(1, 4);
            drink.Decaff = PickBool(rand);
            drink.Wet = PickBool(rand);
            drink.ToGo = PickBool(rand);
            drink.Size = (DrinkSize)PickEnum(typeof(DrinkSize), rand);
            drink.Type = (DrinkType)PickEnum(typeof(DrinkType), rand);
            drink.Milk = (MilkType)PickEnum(typeof(MilkType), rand);
            drink.Sprinkles = (SprinkleType)PickEnum(typeof(SprinkleType), rand);

            // return...
            return drink;
        }

        private int PickEnum(Type enumType, Random rand)
        {
            var options = Enum.GetValues(enumType);
            return ((int[])options)[rand.Next(0, options.Length)];
        }

        private bool PickBool(Random rand)
        {
            return rand.Next(100) < 50;
        }
    }        
}
