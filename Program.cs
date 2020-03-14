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

    public interface ITimerLigth: ILight
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
    #endregion

    public class OverheadLight : ILight
    {
        private bool isOn;
        public bool IsOn() => isOn;
        public void SwitchOff() => isOn = false;
        public void SwitchOn() => isOn = true;

        public override string ToString() => $"The light is {isOn: \"on\", \"off\"}";
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
