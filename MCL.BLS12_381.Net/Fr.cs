using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Text;

namespace MCL.BLS12_381.Net
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct Fr : IEquatable<Fr>
    {
        public const int ByteSize = 32;

        public static Fr Zero = FromInt(0);
        public static Fr One = FromInt(1);

        public static Fr GetRandom()
        {
            var fr = new Fr();
            fr.SetRandom();
            return fr;
        }

        public static Fr FromInt(int x)
        {
            var res = new Fr();
            res.SetInt(x);
            return res;
        }

        public static Fr FromBytes(Span<byte> bytes)
        {
            var res = new Fr();
            res.SetBytes(bytes);
            return res;
        }

        public void Clear()
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrClear.Value(ptr);
                }
            }
        }

        public void SetInt(int x)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrSetInt32.Value(ptr, x);
                }
            }
        }

        [Pure]
        public byte[] ToBytes()
        {
            unsafe
            {
                Span<byte> res = stackalloc byte[ByteSize];
                fixed (Fr* ptr = &this)
                fixed (byte* resPtr = res)
                {
                    var size = MclBls12381.Imports.MclBnFrSerialize.Value(resPtr, ByteSize, ptr);
                    if (size == 0) throw new InvalidOperationException("mclBnFr_serialize failed to serialize Fr");
                }

                return res.ToArray();
            }
        }

        [Pure]
        public byte[] ToBytes(IoMode ioMode)
        {
            unsafe
            {
                const int maxSize = 1024;
                Span<byte> buf = stackalloc byte[maxSize];
                fixed (Fr* ptr = &this)
                fixed (byte* bufPtr = buf)
                {
                    var size = MclBls12381.Imports.MclBnFrGetStr.Value(bufPtr, maxSize, ptr, (int) ioMode);
                    if (size == 0 || size > 1024)
                        throw new InvalidOperationException("mclBnFr_getStr failed to serialize Fr");
                    return buf.Slice(0, (int) size).ToArray();
                }
            }
        }

        public void SetBytes(Span<byte> bytes)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                fixed (byte* bytesPtr = bytes)
                {
                    var size = MclBls12381.Imports.MclBnFrDeserialize.Value(ptr, bytesPtr, (ulong) bytes.Length);
                    if (size == 0) throw new InvalidOperationException("mclBnFr_setStr failed to deserialize Fr");
                }
            }
        }

        public void SetRandom()
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    var error = MclBls12381.Imports.MclBnFrSetByCsprng.Value(ptr);
                    if (error != 0) throw new InvalidOperationException($"mclBnFr_setByCSPRNG error: {error}");
                }
            }
        }

        public bool IsValid()
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnFrIsValid.Value(ptr) == 1;
                }
            }
        }

        public bool IsZero()
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnFrIsZero.Value(ptr) == 1;
                }
            }
        }

        public bool IsOne()
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    return MclBls12381.Imports.MclBnFrIsOne.Value(ptr) == 1;
                }
            }
        }

        public void SetNegationOf(Fr x)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrNeg.Value(ptr, &x);
                }
            }
        }

        public void SetInverseOf(Fr x)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrInv.Value(ptr, &x);
                }
            }
        }

        public void SetSquareOf(Fr x)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrSqr.Value(ptr, &x);
                }
            }
        }

        public void SetAdditionOf(Fr x, Fr y)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrAdd.Value(ptr, &x, &y);
                }
            }
        }

        public void SetSubtractionOf(Fr x, Fr y)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrSub.Value(ptr, &x, &y);
                }
            }
        }

        public void SetMultiplicationOf(Fr x, Fr y)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrMul.Value(ptr, &x, &y);
                }
            }
        }

        public void SetDivisionOf(Fr x, Fr y)
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    MclBls12381.Imports.MclBnFrDiv.Value(ptr, &x, &y);
                }
            }
        }

        public static bool operator ==(Fr x, Fr y)
        {
            unsafe
            {
                return MclBls12381.Imports.MclBnFrIsEqual.Value(&x, &y) != 0;
            }
        }

        public static bool operator !=(Fr x, Fr y)
        {
            return !(x == y);
        }

        public static Fr operator -(Fr x)
        {
            var y = new Fr();
            y.SetNegationOf(x);
            return y;
        }

        public static Fr operator +(Fr x, Fr y)
        {
            var z = new Fr();
            z.SetAdditionOf(x, y);
            return z;
        }

        public static Fr operator -(Fr x, Fr y)
        {
            var z = new Fr();
            z.SetSubtractionOf(x, y);
            return z;
        }

        public static Fr operator *(Fr x, Fr y)
        {
            var z = new Fr();
            z.SetMultiplicationOf(x, y);
            return z;
        }

        public Fr Square()
        {
            var z = new Fr();
            z.SetSquareOf(this);
            return z;
        }

        public static Fr operator /(Fr x, Fr y)
        {
            var z = new Fr();
            z.SetDivisionOf(x, y);
            return z;
        }

        public Fr Inverse()
        {
            var z = new Fr();
            z.SetInverseOf(this);
            return z;
        }

        public bool Equals(Fr other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            return obj is Fr other && Equals(other);
        }

        public override int GetHashCode()
        {
            unsafe
            {
                fixed (Fr* ptr = &this)
                {
                    return ((int*) ptr)[0];
                }
            }
        }

        public override string ToString()
        {
            return $"Fr({Encoding.ASCII.GetString(ToBytes(IoMode.IoSerializeHexStr))})";
        }
    }
}