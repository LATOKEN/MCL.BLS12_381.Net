using System.Linq;
using NUnit.Framework;

namespace MCL.BLS12_381.Net.Test
{
    public class MclTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPolyEvaluationG1()
        {
            const int degree = 100;
            var coeffs = Enumerable.Range(0, degree)
                .Select(i => G1.Generator * Fr.GetRandom())
                .ToArray();
            var pt = Fr.GetRandom();
            var res = G1.Zero;
            for (var i = degree - 1; i >= 0; --i)
                res = res * pt + coeffs[i];
            Assert.AreEqual(res, MclBls12381.EvaluatePolynomial(coeffs, pt));
        }
        
        [Test]
        public void TestPolyEvaluationG2()
        {
            const int degree = 100;
            var coeffs = Enumerable.Range(0, degree)
                .Select(i => G2.Generator * Fr.GetRandom())
                .ToArray();
            var pt = Fr.GetRandom();
            var res = G2.Zero;
            for (var i = degree - 1; i >= 0; --i)
                res = res * pt + coeffs[i];
            Assert.AreEqual(res, MclBls12381.EvaluatePolynomial(coeffs, pt));
        }
        
        [Test]
        public void TestPolyEvaluationFr()
        {
            const int degree = 100;
            var coeffs = Enumerable.Range(0, degree)
                .Select(i => Fr.GetRandom())
                .ToArray();
            var pt = Fr.GetRandom();
            var res = Fr.Zero;
            for (var i = degree - 1; i >= 0; --i)
                res = res * pt + coeffs[i];
            Assert.AreEqual(res, MclBls12381.EvaluatePolynomial(coeffs, pt));
        }

        [Test]
        public void TestPolyInterpolationG1()
        {
            const int degree = 100;
            var coeffs = Enumerable.Range(0, degree)
                .Select(i => G1.Generator * Fr.GetRandom())
                .ToArray();
            var xs = Enumerable.Range(1, degree)
                .Select(i => Fr.GetRandom())
                .ToArray();
            var ys = xs
                .Select(x => MclBls12381.EvaluatePolynomial(coeffs, x))
                .ToArray();
            var intercept = MclBls12381.EvaluatePolynomial(coeffs, Fr.FromInt(0));
            Assert.AreEqual(intercept, MclBls12381.LagrangeInterpolate(xs, ys));
        }
        
        [Test]
        public void TestPolyInterpolationG2()
        {
            const int degree = 100;
            var coeffs = Enumerable.Range(0, degree)
                .Select(i => G2.Generator * Fr.GetRandom())
                .ToArray();
            var xs = Enumerable.Range(1, degree)
                .Select(i => Fr.GetRandom())
                .ToArray();
            var ys = xs
                .Select(x => MclBls12381.EvaluatePolynomial(coeffs, x))
                .ToArray();
            var intercept = MclBls12381.EvaluatePolynomial(coeffs, Fr.FromInt(0));
            Assert.AreEqual(intercept, MclBls12381.LagrangeInterpolate(xs, ys));
        }
        
        [Test]
        public void TestPolyInterpolationFr()
        {
            const int degree = 100;
            var coeffs = Enumerable.Range(0, degree)
                .Select(i => Fr.GetRandom())
                .ToArray();
            var xs = Enumerable.Range(1, degree)
                .Select(i => Fr.GetRandom())
                .ToArray();
            var ys = xs
                .Select(x => MclBls12381.EvaluatePolynomial(coeffs, x))
                .ToArray();
            var intercept = MclBls12381.EvaluatePolynomial(coeffs, Fr.FromInt(0));
            Assert.AreEqual(intercept, MclBls12381.LagrangeInterpolate(xs, ys));
        }
        
        [Test]
        public void TestPowersCalculation()
        {
            var powers = Enumerable.Range(0, 30)
                .Select(i => Fr.FromInt(1 << i))
                .ToArray();
            CollectionAssert.AreEqual(powers, MclBls12381.Powers(Fr.FromInt(2), 30));
        }
    }
}