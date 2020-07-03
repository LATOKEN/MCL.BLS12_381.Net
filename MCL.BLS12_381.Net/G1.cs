using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Text;

namespace MCL.BLS12_381.Net
{
    [StructLayout(LayoutKind.Explicit, Size = 144)]
    public struct G1 : IEquatable<G1>
    {
        public const int ByteSize = 48;
        public static G1 Generator = GetGenerator();
        public static G1 Zero = GetZero();

        public static G1 GetGenerator()
        {
            // Some fixed generator can be obtained via hashing any message
            // (all non trivial elements are generators since group has prime order)
            var res = new G1();
            res.SetHashOf(new byte[] {0xde, 0xad, 0xbe, 0xef});
            return res;
        }

        public static G1 GetZero()
        {
            var res = new G1();
            res.Clear();
            return res;
        }

        public static G1 FromBytes(Span<byte> bytes)
        {
            var res = new G1();
            res.SetBytes(bytes);
            return res;
        }

        public void Clear()
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG1Clear.Value(ptr);
                }
            }
        }

        public void SetHashOf(byte[] bytes)
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                fixed (byte* bytesPtr = bytes)
                {
                    MclBls12381.Imports.MclBnG1HashAndMapTo.Value(ptr, bytesPtr, (ulong) bytes.Length);
                }
            }
        }

        public void SetBytes(Span<byte> bytes)
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                fixed (byte* bytesPtr = bytes)
                {
                    var size = MclBls12381.Imports.MclBnG1Deserialize.Value(ptr, bytesPtr, (ulong) bytes.Length);
                    if (size == 0) throw new InvalidOperationException("mclBnG1_deserialize failed to deserialize G1");
                }
            }
        }

        [Pure]
        public byte[] ToBytes()
        {
            unsafe
            {
                Span<byte> res = stackalloc byte[ByteSize];
                fixed (G1* ptr = &this)
                fixed (byte* resPtr = res)
                {
                    MclBls12381.Imports.MclBnG1Serialize.Value(resPtr, ByteSize, ptr);
                }

                return res.ToArray();
            }
        }

        [Pure]
        public byte[] ToBytes(IoMode ioMode)
        {
            unsafe
            {
                const int maxBufSize = 1024;
                Span<byte> res = stackalloc byte[maxBufSize];
                ulong len;
                fixed (G1* ptr = &this)
                fixed (byte* resPtr = res)
                {
                    len = MclBls12381.Imports.MclBnG1GetStr.Value(resPtr, maxBufSize, ptr, (int) ioMode);
                }

                return res.Slice(0, (int) len).ToArray();
            }
        }

        public bool IsValid()
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnG1IsValid.Value(ptr) == 1;
                }
            }
        }

        public bool IsZero()
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnG1IsZero.Value(ptr) == 1;
                }
            }
        }

        public void SetNegationOf(G1 x)
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG1Neg.Value(ptr, &x);
                }
            }
        }

        public void SetAdditionOf(G1 x, G1 y)
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG1Add.Value(ptr, &x, &y);
                }
            }
        }

        public void SetDoublingOf(G1 x)
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG1Dbl.Value(ptr, &x);
                }
            }
        }

        public void SetSubtractionOf(G1 x, G1 y)
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG1Sub.Value(ptr, &x, &y);
                }
            }
        }

        public void SetMultiplicationOf(G1 x, Fr y)
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG1Mul.Value(ptr, &x, &y);
                }
            }
        }

        public static bool operator ==(G1 x, G1 y)
        {
            if (x.IsZero()) return y.IsZero();
            unsafe
            {
                return MclBls12381.Imports.MclBnG1IsEqual.Value(&x, &y) != 0;
            }
        }

        public static bool operator !=(G1 x, G1 y)
        {
            return !(x == y);
        }

        public static G1 operator +(G1 x, G1 y)
        {
            var z = new G1();
            z.SetAdditionOf(x, y);
            return z;
        }

        public G1 Double()
        {
            var res = new G1();
            res.SetDoublingOf(this);
            return res;
        }

        public static G1 operator -(G1 x, G1 y)
        {
            var z = new G1();
            z.SetSubtractionOf(x, y);
            return z;
        }

        public static G1 operator -(G1 x)
        {
            var z = new G1();
            z.SetNegationOf(x);
            return z;
        }

        public static G1 operator *(G1 x, Fr y)
        {
            var z = new G1();
            z.SetMultiplicationOf(x, y);
            return z;
        }

        public bool Equals(G1 other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            return obj is G1 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unsafe
            {
                fixed (G1* ptr = &this)
                {
                    return ((int*) ptr)[0];
                }
            }
        }

        public override string ToString()
        {
            return $"G1({Encoding.ASCII.GetString(ToBytes(IoMode.IoSerializeHexStr))})";
        }
    }
}