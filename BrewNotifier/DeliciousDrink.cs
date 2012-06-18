using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifier
{
    public class DeliciousDrink
    {
        public int NumShots { get; internal set; }
        public DrinkSize Size { get; internal set; }
        public bool Decaf { get; internal set; }
        public DrinkType Type { get; internal set; }
        public MilkType Milk { get; internal set; }
        public bool Wet { get; internal set; }
        public SprinkleType Sprinkles { get; internal set; }
        public bool ToGo { get; internal set; }

        internal DeliciousDrink()
        {
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(Size);
            builder.Append(" ");

            if (NumShots == 1)
                builder.Append("single-shot");
            else if (NumShots == 2)
                builder.Append("double-shot");
            else if (NumShots == 3)
                builder.Append("triple-shot");
            builder.Append(" ");

            if (Decaf)
                builder.Append("decaf ");

            if (Wet)
                builder.Append("wet ");

            builder.Append(Milk);
            builder.Append(" ");

            builder.Append(Type);

            if (Sprinkles != SprinkleType.None)
            {
                builder.Append(" with ");
                builder.Append(Sprinkles);
            }

            if (ToGo)
                builder.Append(", to go.");
            else
                builder.Append(", for here.");

            return builder.ToString();
        }
    }
}
