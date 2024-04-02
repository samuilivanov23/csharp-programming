using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstExerciseConsoleApp
{
    public abstract class Shape
    {
        public int Colour
        {
            get
            {
                switch (Colour)
                {
                    case 16711680: return 1; break;
                    case 65280: return 2; break;
                    case 255: return 3; break;
                    default: return 1;
                }
            }

            set
            {
                switch (value)
                {
                    case 1: Colour = 16711680; break;
                    case 2: Colour = 65280; break;
                    case 3: Colour = 255; break;
                }
            }
        }

        public abstract double CalculatePerimeter(double sizeASize, double sideBSize);
        public abstract double CalculateSurface(double sizeASize, double sideBSize);
    }
}
