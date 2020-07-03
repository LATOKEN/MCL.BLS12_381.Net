using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MCL.BLS12_381.Net
{
    // ReSharper disable once InconsistentNaming
    [StructLayout(LayoutKind.Explicit, Size = 576)]
    public struct GT : IEquatable<GT>
    {
        private const int ByteSize = 576;

        public static GT Zero = FromInt(0);
        public static GT One = FromInt(1);

        public static GT FromInt(int x)
        {
            var res = new GT();
            res.SetInt(x);
            return res;
        }

        public static GT FromBytes(Span<byte> bytes)
        {
            var res = new GT();
            res.SetBytes(bytes);
            return res;
        }

        public void Clear()
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtClear.Value(ptr);
                }
            }
        }

        public byte[] ToBytes()
        {
            unsafe
            {
                Span<byte> res = stackalloc byte[ByteSize];
                fixed (GT* ptr = &this)
                fixed (byte* resPtr = res)
                {
                    var size = MclBls12381.Imports.MclBnGtSerialize.Value(resPtr, ByteSize, ptr);
                    if (size == 0) throw new InvalidOperationException("mclBnGT_serialize failed to serialize GT");
                }

                return res.ToArray();
            }
        }

        public byte[] ToBytes(IoMode ioMode)
        {
            unsafe
            {
                const int maxSize = 2048;
                Span<byte> buf = stackalloc byte[maxSize];
                fixed (GT* ptr = &this)
                fixed (byte* bufPtr = buf)
                {
                    var size = MclBls12381.Imports.MclBnGtGetStr.Value(bufPtr, maxSize, ptr, (int) ioMode);
                    if (size == 0 || size > maxSize)
                        throw new InvalidOperationException("mclBnGT_getStr failed to serialize GT");
                    return buf.Slice(0, (int) size).ToArray();
                }
            }
        }

        public bool IsZero()
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnGtIsZero.Value(ptr) != 0;
                }
            }
        }

        public bool IsOne()
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnGtIsOne.Value(ptr) != 0;
                }
            }
        }

        public void SetInt(int x)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtSetInt32.Value(ptr, x);
                }
            }
        }

        public void SetBytes(Span<byte> bytes)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                fixed (byte* bytesPtr = bytes)
                {
                    var size = MclBls12381.Imports.MclBnGtDeserialize.Value(ptr, bytesPtr, (ulong) bytes.Length);
                    if (size == 0) throw new InvalidOperationException("mclBnGT_setStr failed to deserialize GT");
                }
            }
        }

        public void SetNegationOf(GT x)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtNeg.Value(ptr, &x);
                }
            }
        }

        public void SetInverseOf(GT x)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtInv.Value(ptr, &x);
                }
            }
        }

        public void SetAdditionOf(GT x, GT y)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtAdd.Value(ptr, &x, &y);
                }
            }
        }

        public void SetSubtractionOf(GT x, GT y)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtSub.Value(ptr, &x, &y);
                }
            }
        }

        public void SetMultiplicationOf(GT x, GT y)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtMul.Value(ptr, &x, &y);
                }
            }
        }

        public void SetDivisionOf(GT x, GT y)
        {
            if (y.IsZero()) throw new InvalidOperationException("GT: Division by zero");
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtDiv.Value(ptr, &x, &y);
                }
            }
        }

        public static bool operator ==(GT x, GT y)
        {
            unsafe
            {
                return MclBls12381.Imports.MclBnGtIsEqual.Value(&x, &y) != 0;
            }
        }

        public static bool operator !=(GT x, GT y)
        {
            return !(x == y);
        }

        public static GT operator -(GT x)
        {
            var y = new GT();
            y.SetNegationOf(x);
            return y;
        }

        public static GT operator +(GT x, GT y)
        {
            var z = new GT();
            z.SetAdditionOf(x, y);
            return z;
        }

        public static GT operator -(GT x, GT y)
        {
            var z = new GT();
            z.SetSubtractionOf(x, y);
            return z;
        }

        public static GT operator *(GT x, GT y)
        {
            var z = new GT();
            z.SetMultiplicationOf(x, y);
            return z;
        }

        public static GT operator /(GT x, GT y)
        {
            var z = new GT();
            z.SetDivisionOf(x, y);
            return z;
        }

        public GT Inverse()
        {
            var z = new GT();
            z.SetInverseOf(this);
            return z;
        }

        private void SetPowOf(GT x, Fr y)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnGtPow.Value(ptr, &x, &y);
                }
            }
        }

        public static GT Pow(GT x, Fr y)
        {
            var g = new GT();
            g.SetPowOf(x, y);
            return g;
        }

        public void SetPairingOf(G1 x, G2 y)
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    MclBls12381.Imports.MclBnPairing.Value(ptr, &x, &y);
                }
            }
        }

        public static GT Pairing(G1 x, G2 y)
        {
            var res = new GT();
            res.SetPairingOf(x, y);
            return res;
        }

        public static GT FinalExp(GT x)
        {
            unsafe
            {
                GT res;
                MclBls12381.Imports.MclBnFinalExp.Value(&res, &x);
                return res;
            }
        }

        public static GT MillerLoop(G1 x, G2 y)
        {
            unsafe
            {
                GT res;
                MclBls12381.Imports.MclBnMillerLoop.Value(&res, &x, &y);
                return res;
            }
        }

        public bool Equals(GT other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            return obj is GT other && Equals(other);
        }

        public override int GetHashCode()
        {
            unsafe
            {
                fixed (GT* ptr = &this)
                {
                    return ((int*) ptr)[0];
                }
            }
        }

        public override string ToString()
        {
            return $"GT({Encoding.ASCII.GetString(ToBytes(IoMode.IoSerializeHexStr))})";
        }
    }
}