using System;

namespace FirstExerciseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle<int> triangle;
            bool successfullInstance = Triangle<int>.GetInstance(5, 6, 7, out triangle);

            if (successfullInstance)
            {
                Console.WriteLine("Triangle Successfully created");
            }
            else
            {
                Console.WriteLine("Could not creat Triangle");
            }

            Console.WriteLine(Square.CalculateSurface(5));

            Circle circle = new Circle();
            Console.WriteLine(circle.CalculatePerimeter(10));
            Console.WriteLine(circle.CalculateSurface(10));
            Console.WriteLine(circle.CheckisElipseShape());

            Rectangle rectangle = new Rectangle();
            Console.WriteLine(rectangle.CalculatePerimeter(10, 20));
            Console.WriteLine(rectangle.CalculateSurface(10, 20));
            Console.WriteLine(rectangle.CheckisElipseShape());
        }
    }
}