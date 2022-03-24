using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wspolbiezne
{
    public class Kalkulator 
    {
        private int liczba;

        public Kalkulator(int a) 
        {
            this.liczba = a;
        }

        public int Add(int b)
        {
            return this.liczba + b;
        }

        public int Substract(int b)
        {
            return this.liczba - b;
        }

        public int Mul(int b)
        {
            return this.liczba * b;
        }

        public int Div(int b)
        {
            if (b == 0)
                throw new ArithmeticException();
            return this.liczba / b;
        }

    }
}
