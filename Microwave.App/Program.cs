using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Button timeAddButton = new Button();
            Button timeSubtractButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);
            powerTube.setPower();

			Light light = new Light(output);

            Buzzer buzzer = new Buzzer(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube, timeAddButton, timeSubtractButton);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker, buzzer);


            ui.setPower();

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();

			timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop. Input 'U' to add 5 seconds or 'L' to subtract.");
            // Wait for input
            var input = " ";
            while (input!="")
            {
                input = System.Console.ReadLine().ToLower();
                if (input=="u")
                {
                    timeAddButton.Press();
                }
                else if (input=="l")
                {
                    timeSubtractButton.Press();
                }
            }
        }
    }
}
