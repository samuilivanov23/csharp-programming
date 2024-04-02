using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstExerciseConsoleApp
{
    public class Circle : Shape, ICheckIsElipseShape
    {
        public override double CalculatePerimeter(double radiusA, double radiusB)
        {
            return 2 * Math.PI * radiusA;
        }

        public double CalculatePerimeter(double radius) 
        {
            return 2 * Math.PI * radius;
        }

        public override double CalculateSurface(double radiusA, double radiusB)
        {
            return Math.PI * radiusA * radiusA;
        }

        public double CalculateSurface(double radius) 
        {
            return Math.PI * radius * radius;
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
