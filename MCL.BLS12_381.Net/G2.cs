using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Text;

namespace MCL.BLS12_381.Net
{
    [StructLayout(LayoutKind.Explicit, Size = 288)]
    public struct G2 : IEquatable<G2>
    {
        public const int ByteSize = 96;
        public static G2 Generator = GetGenerator();
        public static G2 Zero = GetZero();

        public static G2 GetGenerator()
        {
            // Some fixed generator can be obtained via hashing any message
            // (all non trivial elements are generators since group has prime order)
            var res = new G2();
            res.SetHashOf(new byte[] {0xde, 0xad, 0xbe, 0xef});
            return res;
        }

        public static G2 GetZero()
        {
            var res = new G2();
            res.Clear();
            return res;
        }

        public static G2 FromBytes(Span<byte> bytes)
        {
            var res = new G2();
            res.SetBytes(bytes);
            return res;
        }

        public void Clear()
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG2Clear.Value(ptr);
                }
            }
        }

        public void SetHashOf(byte[] bytes)
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                fixed (byte* bytesPtr = bytes)
                {
                    MclBls12381.Imports.MclBnG2HashAndMapTo.Value(ptr, bytesPtr, (ulong) bytes.Length);
                }
            }
        }

        public void SetBytes(Span<byte> bytes)
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                fixed (byte* bytesPtr = bytes)
                {
                    var size = MclBls12381.Imports.MclBnG2Deserialize.Value(ptr, bytesPtr, (ulong) bytes.Length);
                    if (size == 0) throw new InvalidOperationException("mclBnG2_deserialize failed to deserialize G2");
                }
            }
        }

        [Pure]
        public byte[] ToBytes()
        {
            unsafe
            {
                Span<byte> res = stackalloc byte[ByteSize];
                fixed (G2* ptr = &this)
                fixed (byte* resPtr = res)
                {
                    MclBls12381.Imports.MclBnG2Serialize.Value(resPtr, ByteSize, ptr);
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
                fixed (G2* ptr = &this)
                fixed (byte* resPtr = res)
                {
                    len = MclBls12381.Imports.MclBnG2GetStr.Value(resPtr, maxBufSize, ptr, (int) ioMode);
                }

                return res.Slice(0, (int) len).ToArray();
            }
        }

        public bool IsValid()
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnG2IsValid.Value(ptr) == 1;
                }
            }
        }

        public bool IsZero()
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnG2IsZero.Value(ptr) == 1;
                }
            }
        }

        public void SetNegationOf(G2 x)
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG2Neg.Value(ptr, &x);
                }
            }
        }

        public void SetAdditionOf(G2 x, G2 y)
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG2Add.Value(ptr, &x, &y);
                }
            }
        }

        public void SetDoublingOf(G2 x)
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG2Dbl.Value(ptr, &x);
                }
            }
        }

        public void SetSubtractionOf(G2 x, G2 y)
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG2Sub.Value(ptr, &x, &y);
                }
            }
        }

        public void SetMultiplicationOf(G2 x, Fr y)
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    MclBls12381.Imports.MclBnG2Mul.Value(ptr, &x, &y);
                }
            }
        }

        public static bool operator ==(G2 x, G2 y)
        {
            if (x.IsZero()) return y.IsZero();
            unsafe
            {
                return MclBls12381.Imports.MclBnG2IsEqual.Value(&x, &y) != 0;
            }
        }

        public static bool operator !=(G2 x, G2 y)
        {
            return !(x == y);
        }

        public static G2 operator +(G2 x, G2 y)
        {
            var z = new G2();
            z.SetAdditionOf(x, y);
            return z;
        }

        public G2 Double()
        {
            var res = new G2();
            res.SetDoublingOf(this);
            return res;
        }

        public static G2 operator -(G2 x, G2 y)
        {
            var z = new G2();
            z.SetSubtractionOf(x, y);
            return z;
        }

        public static G2 operator -(G2 x)
        {
            var z = new G2();
            z.SetNegationOf(x);
            return z;
        }

        public static G2 operator *(G2 x, Fr y)
        {
            var z = new G2();
            z.SetMultiplicationOf(x, y);
            return z;
        }

        public bool Equals(G2 other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            return obj is G2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unsafe
            {
                fixed (G2* ptr = &this)
                {
                    return ((int*) ptr)[0];
                }
            }
        }

        public override string ToString()
        {
            return $"G2({Encoding.ASCII.GetString(ToBytes(IoMode.IoSerializeHexStr))})";
        }
    }
}