using System.Linq;
using NUnit.Framework;

namespace MCL.BLS12_381.Net.Test
{
    public class FrTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetRandom()
        {
            var rnd = Fr.GetRandom();
            Assert.IsTrue(rnd.IsValid());
            rnd.Clear();
            Assert.AreEqual(Fr.Zero, rnd);
        }

        [Test]
        public void TestZero()
        {
            Assert.IsTrue(Fr.Zero.IsValid());
            Assert.IsTrue(Fr.Zero.IsZero());
            Assert.IsFalse(Fr.Zero.IsOne());
            Assert.AreEqual(Fr.Zero, Fr.Zero);
            Assert.AreEqual(Fr.Zero, Fr.FromInt(0));
            Assert.AreNotEqual(Fr.Zero, Fr.One);
            Assert.IsTrue(Fr.Zero != Fr.One);
            Assert.AreEqual(Fr.Zero + Fr.Zero, Fr.Zero);
            var rnd = Fr.GetRandom();
            Assert.AreEqual(Fr.Zero + rnd, rnd);
            Assert.AreEqual(rnd + Fr.Zero, rnd);
            Assert.AreEqual(Fr.Zero * rnd, Fr.Zero);
            Assert.AreEqual(rnd * Fr.Zero, Fr.Zero);
            Assert.AreEqual(Fr.Zero, Fr.Zero.Inverse()); // NB
        }

        [Test]
        public void TestOne()
        {
            Assert.IsTrue(Fr.One.IsValid());
            Assert.IsFalse(Fr.One.IsZero());
            Assert.IsTrue(Fr.One.IsOne());
            Assert.AreEqual(Fr.One, Fr.One);
            Assert.AreNotEqual(Fr.Zero, Fr.One);
            Assert.IsTrue(Fr.Zero != Fr.One);
            Assert.AreEqual(Fr.One - Fr.One, Fr.Zero);
            var rnd = Fr.GetRandom();
            Assert.AreEqual(Fr.One * rnd, rnd);
            Assert.AreEqual(rnd * Fr.One, rnd);
            Assert.AreEqual(rnd / Fr.One, rnd);
            Assert.AreEqual(Fr.One, Fr.One.Inverse());
        }

        [Test]
        public void TestSimpleArithmetic()
        {
            for (var i = -100; i <= 100; ++i)
            {
                var x = Fr.FromInt(i);
                Assert.AreEqual(Fr.FromInt(-i), -x);
                Assert.AreEqual(Fr.FromInt(i * i), x.Square());
                for (var j = -100; j <= 100; ++j)
                {
                    var y = Fr.FromInt(j);
                    Assert.AreEqual(Fr.FromInt(i + j), x + y);
                    Assert.AreEqual(Fr.FromInt(i * j), x * y);
                    Assert.AreEqual(Fr.FromInt(i - j), x - y);
                    Assert.AreEqual(
                        j != 0 ? Fr.FromInt(i * j / j) : Fr.FromInt(0),
                        x * y / y
                    );
                }
            }
        }

        [Test]
        public void TestHashCode()
        {
            Assert.AreNotEqual(Fr.One.GetHashCode(), Fr.Zero.GetHashCode());
        }

        [Test]
        [Repeat(100)]
        public void SerializationRoundTrip()
        {
            var x = Fr.GetRandom();
            Assert.IsTrue(x.IsValid());
            var serialized = x.ToBytes();
            Assert.AreEqual(serialized.Length, 32);
            var restored = Fr.FromBytes(serialized);
            Assert.AreEqual(x, restored);
        }
    }
}