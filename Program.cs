using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterfacesCsharp8
{

    #region NonUsableInterfaces
    public interface ICustomer
    {
        IEnumerable<IOrder> PreviousOrder{get;}
        DateTime DateJoind {get;}
        DateTime? LastOrder { get; }
        string Name {get;}
        IDictionary<DateTime,string> Reminders {get;}

        //Version1
        public decimal ComputeLoyaltyDiscount()
        {
            DateTime TwoYearsAgo = DateTime.Now.AddYears(-2);
            if ((DateJoind < TwoYearsAgo) && (PreviousOrder.Count() > 10))
            {
                return 0.10m;
            }
            return 0;
        }
    }

    public interface IOrder
    {
        DateTime Purchased {get;}
        decimal Coist {get;}
    }
    #endregion
    
    #region Light Intefaces
    public interface ILight
    {
        void SwitchOn();
        void SwitchOff();
        bool IsOn();
    }

    public interface ITimerLight: ILight
    {
        public async Task TurnOnFor(int duration)
        {
            Console.Write("Using the default method for ItimerLight.TurnOnFor");
            SwitchOn();
            await Task.Delay(duration);
            SwitchOff();
            Console.Write("Completed ItimerLight.TurnOnFor sequence");
        }
    }

    public interface IBlinkingLight : ILight
    {
        public async Task Blink(int duration, int repeatCount)
        {
            Console.WriteLine("Using the default interface method for IBlinkingLight.Blink.");
            for (int count = 0; count < repeatCount; count++)
            {
                SwitchOn();
                await Task.Delay(duration);
                SwitchOff();
                await Task.Delay(duration);
            }
            Console.WriteLine("Done with the default interface method for IBlinkingLight.Blink.");
        }
    }
    #endregion

    public class HalogenLight : ITimerLight
    {
        private enum HalogenLightState
        {
            Off,
            On,
            TimerModeOn
        }

        private HalogenLightState state;
        public void SwitchOn() => state = HalogenLightState.On;
        public void SwitchOff() => state = HalogenLightState.Off;
        public bool IsOn() => state != HalogenLightState.Off;
        public async Task TurnOnFor(int duration)
        {
            Console.WriteLine("Halogen light starting timer function.");
            state = HalogenLightState.TimerModeOn;
            await Task.Delay(duration);
            state = HalogenLightState.Off;
            Console.WriteLine("Halogen light finished custom timer function");
        }

        public override string ToString() => $"The light is {state}";
    }

    public class LEDLight : IBlinkingLight, ITimerLight, ILight
    {
        private bool isOn;
        public void SwitchOn() => isOn = true;
        public void SwitchOff() => isOn = false;
        public bool IsOn() => isOn;
        public async Task Blink(int duration, int repeatCount)
        {
            Console.WriteLine("LED Light starting the Blink function.");
            await Task.Delay(duration * repeatCount);
            Console.WriteLine("LED Light has finished the Blink funtion.");
        }

        public override string ToString() => $"The light is {isOn: \"on\", \"off\"}";
    }


    public class OverheadLight : ILight, ITimerLight, IBlinkingLight
    {
        private bool isOn;
        public bool IsOn() => isOn;
        public void SwitchOff() => isOn = false;
        public void SwitchOn() => isOn = true;

        public override string ToString() => $"The light is {isOn: \"on\", \"off\"}";
    }

    public class ExtraFancyLight : IBlinkingLight, ITimerLight, ILight
    {
        private bool isOn;
        public void SwitchOn() => isOn = true;
        public void SwitchOff() => isOn = false;
        public bool IsOn() => isOn;
        public async Task Blink(int duration, int repeatCount)
        {
            Console.WriteLine("Extra Fancy Light starting the Blink function.");
            await Task.Delay(duration * repeatCount);
            Console.WriteLine("Extra Fancy Light has finished the Blink funtion.");
        }
        public async Task TurnOnFor(int duration)
        {
            Console.WriteLine("Extra Fancy light starting timer function.");
            await Task.Delay(duration);
            Console.WriteLine("Extra Fancy light finished custom timer function");
        }

        public override string ToString() => $"The light is {isOn: \"on\", \"off\"}";
    }

    
    class Program
    {
        private static async Task TestLightCapabilities(ILight light)
        {
            // Perform basic tests:
            light.SwitchOn();
            Console.WriteLine($"\tAfter switching on, the light is {(light.IsOn() ? "on" : "off")}");
            light.SwitchOff();
            Console.WriteLine($"\tAfter switching off, the light is {(light.IsOn() ? "on" : "off")}");

            if (light is ITimerLight timer)
            {
                Console.WriteLine("\tTesting timer function");
                await timer.TurnOnFor(1000);
                Console.WriteLine("\tTimer function completed");
            } 
            else
            {
                Console.WriteLine("\tTimer function not supported.");
            }

            if (light is IBlinkingLight blinker)
            {
                Console.WriteLine("\tTesting blinking function");
                await blinker.Blink(500, 5);
                Console.WriteLine("\tBlink function completed");
            }
            else
            {
                Console.WriteLine("\tBlink function not supported.");
            }
        }

       static async Task Main(string[] args)
       {
            Console.WriteLine("Testing the overhead light");
            var overhead = new OverheadLight();
            await TestLightCapabilities(overhead);
            Console.WriteLine();

            Console.WriteLine("Testing the halogen light");
            var halogen = new HalogenLight();
            await TestLightCapabilities(halogen);
            Console.WriteLine();

            Console.WriteLine("Testing the LED light");
            var led = new LEDLight();
            await TestLightCapabilities(led);
            Console.WriteLine();

            Console.WriteLine("Testing the fancy light");
            var fancy = new ExtraFancyLight();
            await TestLightCapabilities(fancy);
            Console.WriteLine();
        }
    }
}
