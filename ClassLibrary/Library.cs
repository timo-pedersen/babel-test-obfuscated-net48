using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("TestProject")]

namespace ClassLibrary
{
    public class Library
    {
        public Complex[] CorrelateSignals(double[] signal1, double[] signal2)
        {
            Complex[] spectrum1 = Utils.FourierTransform(signal1);
            Complex[] spectrum2 = Utils.FourierTransform(signal2);

            return Utils.Correlate(spectrum1, spectrum2);
        }
    }
}