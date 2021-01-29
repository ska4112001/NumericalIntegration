using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalIntegration
{
    class Calc
    {
		Poly _polynomial;

		public Calc(Poly polynomial)
		{
			_polynomial = polynomial;
		}

		public double GetIntegralTrapeze(double a, double b, int n = 1000)
		{
			double h = (b - a) / n;
			double I = h / 2 * (_polynomial.Calculate(a) + _polynomial.Calculate(b));
			for (int i = 1; i < n; i++)
				I += h * _polynomial.Calculate(a + i * h);

			return I;
		}

		public double GetIntegralSimpson(double a, double b, int n = 1000)
		{
			if (n % 2 != 0) throw new Exception("Value n should be even!");

			double h = (b - a) / n;
			double I = h / 3 * (_polynomial.Calculate(a) + _polynomial.Calculate(b));
			for (int i = 1; i < n; i++)
			{
				int coefficient = (i % 2 == 0) ? 2 : 4;
				I += h / 3 * coefficient * _polynomial.Calculate(a + i * h);
			}

			return I;
		}
	}
}
