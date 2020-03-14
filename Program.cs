using System;
using System.Collections.Generic;

namespace InterfacesCsharp8
{

    public interface ICustomer
    {
        IEnumerable<IOrder> PreviousOrder{get;}
        DateTime DateJoind {get;}
        DateTime? LastOrder { get; }
        string Name {get;}
        IDictionary<DateTime,string> Reminders {get;}
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
