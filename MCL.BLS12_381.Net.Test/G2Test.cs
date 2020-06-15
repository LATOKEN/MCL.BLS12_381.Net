using NUnit.Framework;

namespace MCL.BLS12_381.Net.Test
{
    public class G2Test
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void TestZero()
        {
            Assert.IsTrue(G2.Zero.IsValid());
            Assert.IsTrue(G2.Zero.IsZero());
            Assert.AreEqual(G2.Zero, G2.Zero);
            // Assert.AreNotEqual(G2.Zero, G2.Generator); // TODO: WTF
            // Assert.IsTrue(G2.Zero != G2.Generator);
            Assert.AreEqual(G2.Zero, G2.Zero + G2.Zero);
            var rnd = G2.GetGenerator() * Fr.GetRandom();
            Assert.AreEqual(G2.Zero + rnd, rnd);
            Assert.AreEqual(rnd + G2.Zero, rnd);
            Assert.AreEqual(rnd * Fr.Zero, G2.Zero);
        }

        [Test]
        public void TestGenerator()
        {
            Assert.IsTrue(G2.Generator.IsValid());
            Assert.IsFalse(G2.Generator.IsZero());
            Assert.AreEqual(G2.Generator, G2.Generator);
            // Assert.AreNotEqual(G2.Zero, G2.Generator); // TODO: WTF?
            // Assert.IsTrue(G2.Zero != G2.Generator);
            Assert.AreEqual(G2.Generator - G2.Generator, G2.Zero);
            Assert.AreEqual(G2.Generator.Double(), G2.Generator + G2.Generator);
            Assert.AreEqual(G2.Generator.Double(), G2.Generator * Fr.FromInt(2));
        }

        [Test]
        public void TestSimpleArithmetic()
        {
            for (var i = -100; i <= 100; ++i)
            {
                var x = G2.Generator * Fr.FromInt(i);
                Assert.AreEqual(G2.Generator * Fr.FromInt(-i), -x);
                for (var j = -100; j <= 100; ++j)
                {
                    var y = G2.Generator * Fr.FromInt(j);
                    Assert.AreEqual(G2.Generator * Fr.FromInt(i + j), x + y);
                    Assert.AreEqual(G2.Generator * Fr.FromInt(i - j), x - y);
                    Assert.AreEqual(G2.Generator * Fr.FromInt(i * j), x * Fr.FromInt(j));
                }
            }
        }

        [Test]
        public void TestHashCode()
        {
            Assert.AreNotEqual(G2.Generator.GetHashCode(), G2.Zero.GetHashCode());
        }

        [Test]
        [Repeat(100)]
        public void SerializationRoundTrip()
        {
            var x = G2.Generator * Fr.GetRandom();
            Assert.IsTrue(x.IsValid());
            var serialized = x.ToBytes();
            Assert.AreEqual(96, serialized.Length);
            var restored = G2.FromBytes(serialized);
            Assert.AreEqual(x, restored);
        }
    }
}