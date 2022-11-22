using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput myOutput;
        public Buzzer(IOutput output)
        {
            myOutput = output;
        }
        public void MakeBuzzerSound()
        {
            myOutput.OutputLine("Making Buzzer Sound");
            Console.Beep(659, 205);
            Thread.Sleep(125);
            Console.Beep(659, 205);
            Thread.Sleep(125);
            Console.Beep(659, 205);
            
        }
    }
}
