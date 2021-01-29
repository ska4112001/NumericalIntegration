using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalIntegration
{
    class Poly
    {
		private double[] coefficients;
		public bool IsRPN = false;
		private string function;
		private string posfixForm;

		public Poly(String input)
		{
			input = input.TrimEnd().TrimStart();

			while (input.Contains("  ")) input = input.Replace("  ", " ");

			if (input.Contains("x"))
			{
				IsRPN = true;
				this.function = input;
				this.posfixForm = RPN.InfixToPostfix(input);
				return;
			}

			coefficients = input.Split(' ').Select(Double.Parse).ToArray();

			if (coefficients.Length == 0)
				throw new Exception("Unable to create object. 0 coefficients has been provided");
		}

		public override string ToString()
		{
			if (IsRPN) return this.function;
			if (coefficients == null) return String.Empty;
			if (coefficients.Length == 0) throw new Exception("Unable to format");

			string baseString = "f(x) = ";

			for (int i = 0; i < coefficients.Length; i++)
			{
				baseString += (coefficients[i] < 0 || i == 0) ? coefficients[i].ToString("") : $"+{coefficients[i]}";

				if (i != coefficients.Length - 1)
					baseString += $"x^{coefficients.Length - 1 - i}";
			}

			return baseString;
		}

		public Dictionary<int, double> Calculate()
		{
			if (coefficients == null || coefficients.Length == 0) throw new Exception("Unable to calculate");

			Dictionary<int, double> result = new Dictionary<int, double>(3);

			for (int i = -1; i <= 1; i++)
			{
				double value = 0;
				for (int j = 0; j < coefficients.Length; j++)
				{
					if (j != coefficients.Length - 1)
						value += coefficients[j] * Math.Pow(i, coefficients.Length - 1 - j);
					else
						value += coefficients[j];
				}

				result.Add(i, value);
			}

			return result;
		}

		public double Calculate(double value)
		{
			if (IsRPN) return RPN.Calculate(this.posfixForm, value);
			if (coefficients == null || coefficients.Length == 0) throw new Exception("Unable to calculate");

			double result = 0;
			for (int j = 0; j < coefficients.Length; j++)
			{
				if (j != coefficients.Length - 1)
					result += coefficients[j] * Math.Pow(value, coefficients.Length - 1 - j);
				else
					result += coefficients[j];
			}

			return result;
		}

		public string CalculateDerivative()
		{
			if (coefficients == null) return String.Empty;
			if (coefficients.Length == 0) throw new Exception("Unable to calculate");

			string baseString = "f'(x) = ";

			for (int i = 0; i < coefficients.Length - 1; i++)
			{
				baseString += ((coefficients.Length - 1 - i) * coefficients[i]).ToString("0.0000");

				if (i != coefficients.Length - 2)
					baseString += $"x^{coefficients.Length - 2 - i}";
			}

			return baseString;
		}

		public double CalculateDerivative(double value)
		{
			if (coefficients == null || coefficients.Length == 0) throw new Exception("Unable to calculate");

			double result = 0;

			for (int i = 0; i < coefficients.Length - 1; i++)
			{
				if (i != coefficients.Length - 2)
					result += ((coefficients.Length - 1 - i) * coefficients[i]) * (Math.Pow(value, coefficients.Length - 2 - i));
				else
					result += ((coefficients.Length - 1 - i) * coefficients[i]);
			}

			return result;
		}

		public double CalculateRoot(double[] coefficients, double initialGuess)
		{
			for (int i = 0; i < 100; i++)
			{
				double result = initialGuess;

				initialGuess = (initialGuess - (this.Calculate(initialGuess) / this.CalculateDerivative(initialGuess)));

				double approxError = ((initialGuess - result) / initialGuess) * 100;
				if (approxError == 0)
					return result;
			}

			throw new Exception("Root not found");
		}
	}
}
