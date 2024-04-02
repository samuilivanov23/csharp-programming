using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstExerciseConsoleApp
{
    public class Triangle<T>
    {
        public T Side1 { get; set; }
        public T Side2 { get; set; }
        public T Side3 { get; set; }

        public Triangle(T side1, T side2, T side3)
        {
            this.Side1 = side1;
            this.Side2 = side2;
            this.Side3 = side3;
        }

        private static bool IsValidTriangle(T side1, T side2, T side3)
        {
            dynamic s1 = side1, s2 = side2, s3 = side3;
            if (s1 <= 0 || s2 <= 0 || s3 <= 0) { return false; }
            else if (s1 + s2 <= 0 || s2 + s3 <= s1 || s1 + s3 <= s2) { return false; }

            return true;
        }

        public static bool GetInstance(T side1, T side2, T side3, out Triangle<T> triangle)
        {
            triangle = null;

            if (side1 != null && side2 != null && side3 != null)
            {
                if (typeof(T) == typeof(int) || typeof(T) == typeof(float))
                {
                    dynamic s1 = side1, s2 = side2, s3 = side3;
                    if (Triangle<T>.IsValidTriangle(s1, s2, s3))
                    triangle = new Triangle<T>(side1, side2, side3);
                    return true;
                }
            }

            return false;
        }
    }
}
