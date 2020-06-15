using System;
using NUnit.Framework;

namespace MCL.BLS12_381.Net.Test
{
    public class GtTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestZero()
        {
            var rndGen = new Random(1337);
            Assert.IsTrue(GT.Zero.IsZero());
            Assert.IsFalse(GT.Zero.IsOne());
            Assert.AreEqual(GT.Zero, GT.Zero);
            Assert.AreEqual(GT.Zero, GT.FromInt(0));
            Assert.AreNotEqual(GT.Zero, GT.One);
            Assert.IsTrue(GT.Zero != GT.One);
            Assert.AreEqual(GT.Zero + GT.Zero, GT.Zero);
            var rnd = GT.One * GT.FromInt(rndGen.Next());
            Assert.AreEqual(GT.Zero + rnd, rnd);
            Assert.AreEqual(rnd + GT.Zero, rnd);
            Assert.AreEqual(GT.Zero * rnd, GT.Zero);
            Assert.AreEqual(rnd * GT.Zero, GT.Zero);
            Assert.AreEqual(GT.Zero, GT.Zero.Inverse()); // NB
            rnd.Clear();
            Assert.AreEqual(GT.Zero, rnd);
        }

        [Test]
        public void TestOne()
        {
            var rndGen = new Random(1337);
            Assert.IsFalse(GT.One.IsZero());
            Assert.IsTrue(GT.One.IsOne());
            Assert.AreEqual(GT.One, GT.One);
            Assert.AreNotEqual(GT.Zero, GT.One);
            Assert.IsTrue(GT.Zero != GT.One);
            Assert.AreEqual(GT.One - GT.One, GT.Zero);
            var rnd = GT.One * GT.FromInt(rndGen.Next());
            Assert.AreEqual(GT.One * rnd, rnd);
            Assert.AreEqual(rnd * GT.One, rnd);
            Assert.AreEqual(rnd / GT.One, rnd);
            Assert.AreEqual(GT.One, GT.One.Inverse());
        }

        [Test]
        public void TestSimpleArithmetic()
        {
            for (var i = -100; i <= 100; ++i)
            {
                var x = GT.FromInt(i);
                Assert.AreEqual(GT.FromInt(-i), -x);
                for (var j = -100; j <= 100; ++j)
                {
                    var y = GT.FromInt(j);
                    Assert.AreEqual(GT.FromInt(i + j), x + y);
                    Assert.AreEqual(GT.FromInt(i * j), x * y);
                    Assert.AreEqual(GT.FromInt(i - j), x - y);
                    if (j == 0)
                    {
                        Assert.Throws<InvalidOperationException>(() =>
                        {
                            var unused = x * y / y;
                        });
                    }
                    else
                    {
                        Assert.AreEqual(GT.FromInt(i * j / j), x * y / y);
                    }

                    if (j > 0 && i >= -8 && i <= 8 && j <= 8)
                    {
                        Assert.AreEqual(GT.FromInt((int) Math.Pow(i, j)), GT.Pow(x, Fr.FromInt(j)));
                    }
                }
            }
        }

        [Test]
        public void TestHashCode()
        {
            Assert.AreNotEqual(GT.One.GetHashCode(), GT.Zero.GetHashCode());
        }

        [Test]
        public void TestPairing()
        {
            var fr1 = Fr.GetRandom();
            var fr2 = Fr.GetRandom();
            var rndG1 = G1.Generator * fr1;
            var rndG2 = G2.Generator * fr2;
            Assert.AreEqual(GT.One, GT.Pairing(G1.Zero, G2.Zero));
            Assert.AreEqual(GT.One, GT.Pairing(G1.Zero, rndG2));
            Assert.AreEqual(GT.One, GT.Pairing(rndG1, G2.Zero));
            var generatorsPairing = GT.Pairing(G1.Generator, G2.Generator);
            Assert.AreEqual(GT.Pow(generatorsPairing, fr1 * fr2), GT.Pairing(rndG1, rndG2));
        }

        [Test]
        [Repeat(100)]
        public void SerializationRoundTrip()
        {
            var rndGen = new Random(1337);
            var x = GT.FromInt(rndGen.Next());
            var serialized = x.ToBytes();
            Assert.AreEqual(serialized.Length, 576);
            var restored = GT.FromBytes(serialized);
            Assert.AreEqual(x, restored);
        }
    }
}