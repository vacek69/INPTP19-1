using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;


namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const double epsilon = 0.0001;
        
        private static double xmin, xmax, ymin, ymax, xstep, ystep ;
        
        private static Bitmap image;
        
        private static Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        private static List<ComplexNumber> roots;

        static void Main(string[] args)
        {
            Initialize();
            Polynome polynome = InitializePolynome();
            Polynome polynomeDerivate = polynome.Derive();

            Console.WriteLine(polynome);
            Console.WriteLine(polynomeDerivate);
            
            ProcessImage(polynome, polynomeDerivate);
            SaveImage();
        }

        static void Initialize()
        {
            image = new Bitmap(300, 300);
            xmin = -1.5;
            xmax = 1.5;
            ymin = -1.5;
            ymax = 1.5;

            xstep = (xmax - xmin) / image.Width;
            ystep = (ymax - ymin) / image.Height;

            roots = new List<ComplexNumber>();
        }

        static Polynome InitializePolynome()
        {
            return new Polynome()
            {
                Coeficients =
                {
                    new ComplexNumber() { Real = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber() { Real = 1 }
                }
            };
        }

        private static void ProcessImage(Polynome polynome, Polynome polynomeDerivate)
        {
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    ComplexNumber point = CalculateWorldCoordinatesOfPoint(i, j);
                    int iteration = CalculateNewtonIteration(polynome, polynomeDerivate, ref point);
                    int rootNumber = FindRootNumber(point);
                    Color selectedColor = SelectColofOfPixel(iteration, rootNumber);
                    image.SetPixel(j, i, selectedColor);
                }
            }
        }

        private static int FindRootNumber(ComplexNumber point)
        {
            var known = false;
            var rootNumber = 0;
            for(int i = 0; i < roots.Count(); i++)
            {
                if (Math.Pow(point.Real - roots[i].Real, 2) + Math.Pow(point.Imaginary - roots[i].Imaginary, 2) <= 0.01)
                {
                    known = true;
                    rootNumber = i;
                }
            }
            if (!known)
            {
                roots.Add(point);
                rootNumber = roots.Count();
            }
            return rootNumber;
        }

        private static Color SelectColofOfPixel(int iteration, int rootNumber)
        {
            Color selectedColor = colors[rootNumber % colors.Length];
            selectedColor = Color.FromArgb(
                Math.Min(Math.Max(0, selectedColor.R - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.G - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.B - iteration * 2), 255)
                );
            return selectedColor;
        }

        private static ComplexNumber CalculateWorldCoordinatesOfPoint(int xCoordinate, int yCoordinate)
        {
            double x = xmin + yCoordinate * xstep;
            double y = ymin + xCoordinate * ystep;

            return new ComplexNumber()
            {
                Real = x == 0 ? epsilon : x,
                Imaginary = y == 0 ? epsilon : y,
            };
        }

        private static void SaveImage()
        {
            image.Save("../../../out.png");
        }

        private static int CalculateNewtonIteration(Polynome polynome, Polynome polynomeDerivate, ref ComplexNumber worldCoordinatesOfPixel)
        {
            int cycleCount = 0;
            for (int i = 0; i < 30; i++)
            {
                ComplexNumber diff = polynome.Eval(worldCoordinatesOfPixel).Divide(polynomeDerivate.Eval(worldCoordinatesOfPixel));
                worldCoordinatesOfPixel = worldCoordinatesOfPixel.Subtract(diff);

                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                {
                    i--;
                }
                cycleCount++;
            }

            return cycleCount;
        }
    }

    

    
}
