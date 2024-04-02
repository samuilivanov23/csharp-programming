using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstExerciseConsoleApp
{
    public sealed class Square : Rectangle, ICheckIsElipseShape
    {
        public override double CalculatePerimeter(double sideASize, double sideBSize)
        {
            return sideASize * 2 + sideBSize * 2;
        }

        public override double CalculateSurface(double sideASize, double sideBSize)
        {
            return sideASize * sideBSize;
        }

        public static double CalculateSurface(double sideSize)
        {
            return sideSize * sideSize;
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
