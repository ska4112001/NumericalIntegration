using System;

namespace NumericalIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
				Console.WriteLine("Please write the coefficients of polynomial function:");
				String input = Console.ReadLine();
				Poly polynomial = new Poly(input);
				Console.WriteLine("The function you've entered:");
				Console.WriteLine(polynomial);
				Console.WriteLine("Please write the boundaries of integration:");
				Console.Write("a = ");
				double a = Int32.Parse(Console.ReadLine());
				Console.Write("b = ");
				double b = Int32.Parse(Console.ReadLine());
				Calc calc = new Calc(polynomial);
				Console.WriteLine("Ingeration by trapezoidal method: " + calc.GetIntegralTrapeze(a, b));
				Console.WriteLine("Ingeration by Simpson's method: " + calc.GetIntegralSimpson(a, b));

			}
			catch (Exception e)
			{
				Console.WriteLine("Error occured: " + e.Message);
			}
		}
    }
}
