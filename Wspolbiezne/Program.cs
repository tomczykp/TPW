using System;

namespace Wspolbiezne
{
    class Program
    {
        static void Main(string[] args)
        {
            Kalkulator kalkulator = new(10);

            System.Console.WriteLine(kalkulator.Add(5));
        }
    }
}