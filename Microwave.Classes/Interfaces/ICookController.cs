using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    public interface ICookController
    {
        int maxPower { get; }
        void setPower();
        void StartCooking(int power, int time);
        void Stop();
    }
}
