using System;
using System.Collections.Generic;
using System.Linq;

namespace InterfacesCsharp8
{

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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
