using System;

namespace InterfacesCsharp8
{

    public interface ICustomer
    {
        IEnumerable<IOrder> PreviousOrder{get;}

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
