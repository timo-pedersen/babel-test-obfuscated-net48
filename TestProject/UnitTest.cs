using ClassLibrary;
using System;
using System.Linq;
using System.Numerics;
using System.Text;
using Xunit.Abstractions;
using Xunit;

namespace TestProject
{
    public class UnitTest
    {
        private readonly ITestOutputHelper output;

        public UnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test1()
        {
            double[] signal1 = Utils.GenerateSignal(50, 0.2, Math.PI);
            Plot(signal1);

            double[] signal2 = Utils.GenerateSignal(50, 2, 0);
            Plot(signal2);
        }

        [Fact]
        public void Test2()
        {
            double[] signal1 = Utils.GenerateSignal(50, 0.2, Math.PI);
            Complex[] fourier1 = Utils.FourierTransform(signal1);
            Plot(fourier1);

            double[] signal2 = Utils.GenerateSignal(50, 2, 0);
            Complex[] fourier2 = Utils.FourierTransform(signal2);
            Plot(fourier2);
        }

        [Fact]
        public void Test3()
        {
            Library lib = new Library();
            double[] signal1 = Utils.GenerateSignal(50, 0.2, Math.PI);
            double[] signal2 = Utils.GenerateSignal(50, 2, 0);

            Complex[] correlation = lib.CorrelateSignals(signal1, signal2);
            Plot(correlation);
        }

        private void Plot(Complex[] data)
        {
            double[] abs = data.Select(c => Complex.Abs(c)).ToArray();
            double max = abs.Max();           

            for (int i = 0; i < abs.Length; i++)
            {
                abs[i] = abs[i] / max;
            }

            Plot(abs);
        }

        private void Plot(double[] data)
        {
            double max = Math.Ceiling(data.Max());

            int height = 10;
            double deltay = max / height;

            for (int yi = 10; yi > 0; yi -= 1)
            {
                double y = yi * deltay;

                StringBuilder line = new StringBuilder();
                line.AppendFormat("{0:0.0,3}|", y);

                for (int x = 0; x < data.Length; x++)
                {
                    double val = data[x];
                    if (y - deltay < val && y > val)
                        line.Append("*");
                    else
                        line.Append(" ");
                }

                output.WriteLine(line.ToString());
            }
            
            output.WriteLine("0.0 +" + new string('-', 3 + data.Length));

            StringBuilder axes = new StringBuilder();
            axes.Append(new String(' ', 4));
            for (int i = 0; i <= data.Length; i += 5)
            {
                axes.AppendFormat("{0,-5}", i);
            }

            output.WriteLine(axes.ToString());
        }
    }
}