using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
	public class PowerTube : IPowerTube
	{
		private IOutput myOutput;

		private bool IsOn = false;

		public int Power { get; set; } = 50;

		public PowerTube(IOutput output)
		{
			myOutput = output;

			Console.Write("How much power is needed: ");
			string p = Console.ReadLine();
			Power = int.TryParse(p, out int pow) ? pow : throw new ArgumentException($"{p} is not a number");
		}

		public void TurnOn(int power)
		{
			if (power < 1 || 700 < power)
			{
				throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 700 (incl.)");
			}

			if (IsOn)
			{
				throw new ApplicationException("PowerTube.TurnOn: is already on");
			}

			myOutput.OutputLine($"PowerTube works with {power}");
			IsOn = true;
		}

		public void TurnOff()
		{
			if (IsOn)
			{
				myOutput.OutputLine($"PowerTube turned off");
			}

			IsOn = false;
		}
	}
}