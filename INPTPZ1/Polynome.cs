using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1
{
    public class Polynome
    {
        public List<ComplexNumber> Coeficients { get; set; }

        public Polynome()
        {
            Coeficients = new List<ComplexNumber>();
        }

        public Polynome Derive()
        {
            Polynome polynome = new Polynome();
            for (int i = 1; i < Coeficients.Count; i++)
            {
                polynome.Coeficients.Add(Coeficients[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return polynome;
        }

        public ComplexNumber Eval(ComplexNumber otherComplexNumber)
        {
            ComplexNumber zeroComplexNumber = ComplexNumber.Zero;
            for (int i = 0; i < this.Coeficients.Count; i++)
            {
                ComplexNumber coeficientsComplexNumber = this.Coeficients[i];
                ComplexNumber otherComplexNumberCopy = otherComplexNumber;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                    {
                        otherComplexNumberCopy = otherComplexNumberCopy.Multiply(otherComplexNumber);
                    }
                    
                    coeficientsComplexNumber = coeficientsComplexNumber.Multiply(otherComplexNumberCopy);
                }

                zeroComplexNumber = zeroComplexNumber.Add(coeficientsComplexNumber);
            }

            return zeroComplexNumber;
        }

        public override string ToString()
        {
            string s = String.Empty;
            for (int i = 0; i < Coeficients.Count; i++)
            {
                s += Coeficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i < Coeficients.Count - 1)
                {
                    s += " + ";
                }
            }
            return s;
        }
    }
}
