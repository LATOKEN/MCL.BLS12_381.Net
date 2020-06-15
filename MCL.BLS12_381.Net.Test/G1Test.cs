using NUnit.Framework;

namespace MCL.BLS12_381.Net.Test
{
    public class G1Test
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void TestZero()
        {
            Assert.IsTrue(G1.Zero.IsValid());
            Assert.IsTrue(G1.Zero.IsZero());
            Assert.AreEqual(G1.Zero, G1.Zero);
            // Assert.AreNotEqual(G1.Zero, G1.Generator); // TODO: WTF
            // Assert.IsTrue(G1.Zero != G1.Generator);
            Assert.AreEqual(G1.Zero, G1.Zero + G1.Zero);
            var rnd = G1.GetGenerator() * Fr.GetRandom();
            Assert.AreEqual(G1.Zero + rnd, rnd);
            Assert.AreEqual(rnd + G1.Zero, rnd);
            Assert.AreEqual(rnd * Fr.Zero, G1.Zero);
        }

        [Test]
        public void TestGenerator()
        {
            Assert.IsTrue(G1.Generator.IsValid());
            Assert.IsFalse(G1.Generator.IsZero());
            Assert.AreEqual(G1.Generator, G1.Generator);
            // Assert.AreNotEqual(G1.Zero, G1.Generator); // TODO: WTF?
            // Assert.IsTrue(G1.Zero != G1.Generator);
            Assert.AreEqual(G1.Generator - G1.Generator, G1.Zero);
            Assert.AreEqual(G1.Generator.Double(), G1.Generator + G1.Generator);
            Assert.AreEqual(G1.Generator.Double(), G1.Generator * Fr.FromInt(2));
        }

        [Test]
        public void TestSimpleArithmetic()
        {
            for (var i = -100; i <= 100; ++i)
            {
                var x = G1.Generator * Fr.FromInt(i);
                Assert.AreEqual(G1.Generator * Fr.FromInt(-i), -x);
                for (var j = -100; j <= 100; ++j)
                {
                    var y = G1.Generator * Fr.FromInt(j);
                    Assert.AreEqual(G1.Generator * Fr.FromInt(i + j), x + y);
                    Assert.AreEqual(G1.Generator * Fr.FromInt(i - j), x - y);
                    Assert.AreEqual(G1.Generator * Fr.FromInt(i * j), x * Fr.FromInt(j));
                }
            }
        }

        [Test]
        public void TestHashCode()
        {
            Assert.AreNotEqual(G1.Generator.GetHashCode(), G1.Zero.GetHashCode());
        }

        [Test]
        [Repeat(100)]
        public void SerializationRoundTrip()
        {
            var x = G1.Generator * Fr.GetRandom();
            Assert.IsTrue(x.IsValid());
            var serialized = x.ToBytes();
            Assert.AreEqual(48, serialized.Length);
            var restored = G1.FromBytes(serialized);
            Assert.AreEqual(x, restored);
        }
    }
}