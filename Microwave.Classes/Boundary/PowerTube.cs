using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        public int maxPower { get; set; } = 700;

        public PowerTube(IOutput output)
        {
            myOutput = output;
		}

        public void setPower()
        {
			Console.Write("How much power is needed: ");
			string p = Console.ReadLine();
			maxPower = int.TryParse(p, out int pow) ? pow : throw new ArgumentException($"{p} is not a number");
		}

        public void TurnOn(int power)
        {
            if (power < 1 || maxPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, $"Must be between 1 and {maxPower} (incl.)");
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