using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1
{
    public class ComplexNumber
    {
        public double Real { get; set; }

        public double Imaginary { get; set; }

        public static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public ComplexNumber Multiply(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                Real = this.Real * other.Real - this.Imaginary * other.Imaginary,
                Imaginary = this.Real * other.Imaginary + this.Imaginary * other.Real
            };
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                Real = this.Real + other.Real,
                Imaginary = this.Imaginary + other.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                Real = this.Real - other.Real,
                Imaginary = this.Imaginary - other.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            ComplexNumber complexNumberDivided = this.Multiply(new ComplexNumber() { Real = other.Real, Imaginary = -other.Imaginary });
            double divisor = other.Real * other.Real + other.Imaginary * other.Imaginary;

            return new ComplexNumber()
            {
                Real = complexNumberDivided.Real / divisor,
                Imaginary = complexNumberDivided.Imaginary / divisor
            };
        }
    }
}
