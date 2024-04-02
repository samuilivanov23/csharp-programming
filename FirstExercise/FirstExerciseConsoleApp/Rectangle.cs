using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstExerciseConsoleApp
{
    public class Rectangle : Shape, ICheckIsElipseShape
    {
        public override double CalculatePerimeter(double sideASize, double sideBSize)
        {
            return (2 * sideASize) + (2 * sideBSize);
        }

        public override double CalculateSurface(double sideASize, double sideBSize)
        {
            return sideASize * sideBSize;
        }

        public bool CheckisElipseShape()
        {
            if (this.GetType().ToString() == "Elipse")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
