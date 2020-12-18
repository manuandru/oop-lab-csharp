namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double re;
        private readonly double im;
        private const double difference = 0.00001;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real
        {
            get
            {
                return this.re;
            }
        }

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary
        {
            get
            {
                return this.im;
            }
        }

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus
        {
            get
            {
                return Math.Sqrt(Math.Pow(this.re, 2) + Math.Pow(this.im, 2));
            }
        }

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase
        {
            get
            {
                return Math.Atan2(this.im, this.re);
            }
        }

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            return $"Z = {this.re} + {this.im}i";
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            return (Math.Abs(this.re - other.Real) <= difference )
                && (Math.Abs(this.im - other.Imaginary) <= difference );
        }

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            return obj is IComplex i ? this.Equals(i) : false;
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return HashCode.Combine(re, im);
        }
    }
}
