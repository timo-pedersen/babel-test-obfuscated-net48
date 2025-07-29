using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("TestProject")]


namespace ClassLibrary
{
    internal class Utils
    {
        public static double[] GenerateSignal(int length, double frequency, double phase)
        {
            double[] signal = new double[length];
            for (int i = 0; i < length; i++)
            {
                signal[i] = 1 + Math.Cos(((2 * Math.PI * frequency * i) / length) + phase);
            }
            return signal;
        }

        public static Complex[] FourierTransform(double[] signal)
        {
            int n = signal.Length;
            Complex[] spectrum = new Complex[n];

            if (n == 1)
            {
                spectrum[0] = new Complex(signal[0], 0);
                return spectrum;
            }

            double[] even = new double[n / 2];
            double[] odd = new double[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                even[i] = signal[2 * i];
                odd[i] = signal[2 * i + 1];
            }

            Complex[] evenSpectrum = FourierTransform(even);
            Complex[] oddSpectrum = FourierTransform(odd);

            for (int k = 0; k < n / 2; k++)
            {
                double kth = -2 * k * Math.PI / n;
                Complex wk = new Complex(Math.Cos(kth), Math.Sin(kth));
                spectrum[k] = evenSpectrum[k] + wk * oddSpectrum[k];
                spectrum[k + n / 2] = evenSpectrum[k] - wk * oddSpectrum[k];
            }

            return spectrum;
        }

        public static Complex[] Correlate(Complex[] spectrum1, Complex[] spectrum2)
        {
            int n = spectrum1.Length;
            Complex[] correlation = new Complex[n];

            for (int i = 0; i < n; i++)
            {
                correlation[i] = spectrum1[i] * Complex.Conjugate(spectrum2[i]);
            }

            return FourierTransform(correlation.Select(c => c.Real).ToArray());
        }
    }
}
