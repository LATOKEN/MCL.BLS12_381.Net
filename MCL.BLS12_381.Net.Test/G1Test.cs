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
            Assert.AreNotEqual(G1.Zero, G1.Generator);
            Assert.IsTrue(G1.Zero != G1.Generator);
            Assert.AreEqual(G1.Zero, G1.Zero + G1.Zero);
            var rnd = G1.GetGenerator() * Fr.GetRandom();
            Assert.AreEqual(G1.Zero + rnd, rnd);
            Assert.AreEqual(rnd + G1.Zero, rnd);
            Assert.AreEqual(rnd * Fr.Zero, G1.Zero);
            Assert.AreEqual(
                "G1(000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)",
                G1.Zero.ToString()
            );
            Assert.AreNotEqual(G1.Zero, G2.Zero);
        }

        [Test]
        public void TestGenerator()
        {
            Assert.IsTrue(G1.Generator.IsValid());
            Assert.IsFalse(G1.Generator.IsZero());
            Assert.AreEqual(G1.Generator, G1.Generator);
            Assert.AreNotEqual(G1.Zero, G1.Generator);
            Assert.AreNotEqual(G1.Generator, G1.Generator * Fr.FromInt(2));
            Assert.IsTrue(G1.Zero != G1.Generator);
            Assert.AreEqual(G1.Generator - G1.Generator, G1.Zero);
            Assert.AreEqual(G1.Generator.Double(), G1.Generator + G1.Generator);
            Assert.AreEqual(G1.Generator.Double(), G1.Generator * Fr.FromInt(2));
            Assert.AreEqual(
                "G1(e9328f8eb8185341f22adaf2bf41f66258d97b2b5b2dbd2c27a77c81d9b5d76dd119bf7b1cd5d57b1273f9c4a654540e)",
                G1.Generator.ToString()
            );
            Assert.AreNotEqual(G1.Generator, G2.Generator);
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