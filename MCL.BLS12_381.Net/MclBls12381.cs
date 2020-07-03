using System;

namespace MCL.BLS12_381.Net
{
    public class MclBls12381
    {
        internal readonly Lazy<mclBn_init> MclBnInit;

        internal readonly Lazy<mclBnFr_clear> MclBnFrClear;
        internal readonly Lazy<mclBnFr_setInt32> MclBnFrSetInt32;
        internal readonly Lazy<mclBnFr_setByCSPRNG> MclBnFrSetByCsprng;
        internal readonly Lazy<mclBnFr_setStr> MclBnFrSetStr;
        internal readonly Lazy<mclBnFr_getStr> MclBnFrGetStr;
        internal readonly Lazy<mclBnFr_isValid> MclBnFrIsValid;
        internal readonly Lazy<mclBnFr_isEqual> MclBnFrIsEqual;
        internal readonly Lazy<mclBnFr_isZero> MclBnFrIsZero;
        internal readonly Lazy<mclBnFr_isOne> MclBnFrIsOne;
        internal readonly Lazy<mclBnFr_neg> MclBnFrNeg;
        internal readonly Lazy<mclBnFr_inv> MclBnFrInv;
        internal readonly Lazy<mclBnFr_sqr> MclBnFrSqr;
        internal readonly Lazy<mclBnFr_add> MclBnFrAdd;
        internal readonly Lazy<mclBnFr_sub> MclBnFrSub;
        internal readonly Lazy<mclBnFr_mul> MclBnFrMul;
        internal readonly Lazy<mclBnFr_div> MclBnFrDiv;
        internal readonly Lazy<mclBnFr_serialize> MclBnFrSerialize;
        internal readonly Lazy<mclBnFr_deserialize> MclBnFrDeserialize;

        internal readonly Lazy<mclBnG1_clear> MclBnG1Clear;
        internal readonly Lazy<mclBnG1_isValid> MclBnG1IsValid;
        internal readonly Lazy<mclBnG1_isEqual> MclBnG1IsEqual;
        internal readonly Lazy<mclBnG1_isZero> MclBnG1IsZero;
        internal readonly Lazy<mclBnG1_neg> MclBnG1Neg;
        internal readonly Lazy<mclBnG1_dbl> MclBnG1Dbl;
        internal readonly Lazy<mclBnG1_add> MclBnG1Add;
        internal readonly Lazy<mclBnG1_sub> MclBnG1Sub;
        internal readonly Lazy<mclBnG1_mul> MclBnG1Mul;
        internal readonly Lazy<mclBnG1_serialize> MclBnG1Serialize;
        internal readonly Lazy<mclBnG1_deserialize> MclBnG1Deserialize;
        internal readonly Lazy<mclBnG1_getStr> MclBnG1GetStr;
        internal readonly Lazy<mclBnG1_hashAndMapTo> MclBnG1HashAndMapTo;

        internal readonly Lazy<mclBnG2_clear> MclBnG2Clear;
        internal readonly Lazy<mclBnG2_isValid> MclBnG2IsValid;
        internal readonly Lazy<mclBnG2_isEqual> MclBnG2IsEqual;
        internal readonly Lazy<mclBnG2_isZero> MclBnG2IsZero;
        internal readonly Lazy<mclBnG2_neg> MclBnG2Neg;
        internal readonly Lazy<mclBnG2_dbl> MclBnG2Dbl;
        internal readonly Lazy<mclBnG2_add> MclBnG2Add;
        internal readonly Lazy<mclBnG2_sub> MclBnG2Sub;
        internal readonly Lazy<mclBnG2_mul> MclBnG2Mul;
        internal readonly Lazy<mclBnG2_serialize> MclBnG2Serialize;
        internal readonly Lazy<mclBnG2_deserialize> MclBnG2Deserialize;
        internal readonly Lazy<mclBnG2_getStr> MclBnG2GetStr;
        internal readonly Lazy<mclBnG2_hashAndMapTo> MclBnG2HashAndMapTo;

        internal readonly Lazy<mclBnGT_clear> MclBnGtClear;
        internal readonly Lazy<mclBnGT_setInt32> MclBnGtSetInt32;
        internal readonly Lazy<mclBnGT_isEqual> MclBnGtIsEqual;
        internal readonly Lazy<mclBnGT_isZero> MclBnGtIsZero;
        internal readonly Lazy<mclBnGT_isOne> MclBnGtIsOne;
        internal readonly Lazy<mclBnGT_neg> MclBnGtNeg;
        internal readonly Lazy<mclBnGT_inv> MclBnGtInv;
        internal readonly Lazy<mclBnGT_add> MclBnGtAdd;
        internal readonly Lazy<mclBnGT_sub> MclBnGtSub;
        internal readonly Lazy<mclBnGT_mul> MclBnGtMul;
        internal readonly Lazy<mclBnGT_div> MclBnGtDiv;
        internal readonly Lazy<mclBnGT_pow> MclBnGtPow;
        internal readonly Lazy<mclBnGT_serialize> MclBnGtSerialize;
        internal readonly Lazy<mclBnGT_deserialize> MclBnGtDeserialize;
        internal readonly Lazy<mclBnGT_getStr> MclBnGtGetStr;

        internal readonly Lazy<mclBn_pairing> MclBnPairing;
        internal readonly Lazy<mclBn_finalExp> MclBnFinalExp;
        internal readonly Lazy<mclBn_millerLoop> MclBnMillerLoop;
        internal readonly Lazy<mclBn_G2LagrangeInterpolation> MclBnG2LagrangeInterpolation;
        internal readonly Lazy<mclBn_G1LagrangeInterpolation> MclBnG1LagrangeInterpolation;
        internal readonly Lazy<mclBn_FrLagrangeInterpolation> MclBnFrLagrangeInterpolation;
        internal readonly Lazy<mclBn_FrEvaluatePolynomial> MclBnFrEvaluatePolynomial;
        internal readonly Lazy<mclBn_G1EvaluatePolynomial> MclBnG1EvaluatePolynomial;
        internal readonly Lazy<mclBn_G2EvaluatePolynomial> MclBnG2EvaluatePolynomial;

        const string Lib = "mclbn384_256";

        private static readonly Lazy<string> LibPathLazy = new Lazy<string>(() => LibPathResolver.Resolve(Lib));
        private static readonly Lazy<IntPtr> LibPtr = new Lazy<IntPtr>(() => LoadLibNative.LoadLib(LibPathLazy.Value));

        internal static MclBls12381 Imports = new MclBls12381();

        private MclBls12381()
        {
            // load all delegates
            MclBnInit = LazyDelegate<mclBn_init>();

            MclBnFrClear = LazyDelegate<mclBnFr_clear>();
            MclBnFrSetInt32 = LazyDelegate<mclBnFr_setInt32>();
            MclBnFrSetByCsprng = LazyDelegate<mclBnFr_setByCSPRNG>();
            MclBnFrSetStr = LazyDelegate<mclBnFr_setStr>();
            MclBnFrGetStr = LazyDelegate<mclBnFr_getStr>();
            MclBnFrIsValid = LazyDelegate<mclBnFr_isValid>();
            MclBnFrIsEqual = LazyDelegate<mclBnFr_isEqual>();
            MclBnFrIsZero = LazyDelegate<mclBnFr_isZero>();
            MclBnFrIsOne = LazyDelegate<mclBnFr_isOne>();
            MclBnFrNeg = LazyDelegate<mclBnFr_neg>();
            MclBnFrInv = LazyDelegate<mclBnFr_inv>();
            MclBnFrSqr = LazyDelegate<mclBnFr_sqr>();
            MclBnFrAdd = LazyDelegate<mclBnFr_add>();
            MclBnFrSub = LazyDelegate<mclBnFr_sub>();
            MclBnFrMul = LazyDelegate<mclBnFr_mul>();
            MclBnFrDiv = LazyDelegate<mclBnFr_div>();
            MclBnFrSerialize = LazyDelegate<mclBnFr_serialize>();
            MclBnFrDeserialize = LazyDelegate<mclBnFr_deserialize>();

            MclBnG1Clear = LazyDelegate<mclBnG1_clear>();
            MclBnG1IsValid = LazyDelegate<mclBnG1_isValid>();
            MclBnG1IsEqual = LazyDelegate<mclBnG1_isEqual>();
            MclBnG1IsZero = LazyDelegate<mclBnG1_isZero>();
            MclBnG1Neg = LazyDelegate<mclBnG1_neg>();
            MclBnG1Dbl = LazyDelegate<mclBnG1_dbl>();
            MclBnG1Add = LazyDelegate<mclBnG1_add>();
            MclBnG1Sub = LazyDelegate<mclBnG1_sub>();
            MclBnG1Mul = LazyDelegate<mclBnG1_mul>();
            MclBnG1Serialize = LazyDelegate<mclBnG1_serialize>();
            MclBnG1Deserialize = LazyDelegate<mclBnG1_deserialize>();
            MclBnG1GetStr = LazyDelegate<mclBnG1_getStr>();
            MclBnG1HashAndMapTo = LazyDelegate<mclBnG1_hashAndMapTo>();

            MclBnG2Clear = LazyDelegate<mclBnG2_clear>();
            MclBnG2IsValid = LazyDelegate<mclBnG2_isValid>();
            MclBnG2IsEqual = LazyDelegate<mclBnG2_isEqual>();
            MclBnG2IsZero = LazyDelegate<mclBnG2_isZero>();
            MclBnG2Neg = LazyDelegate<mclBnG2_neg>();
            MclBnG2Dbl = LazyDelegate<mclBnG2_dbl>();
            MclBnG2Add = LazyDelegate<mclBnG2_add>();
            MclBnG2Sub = LazyDelegate<mclBnG2_sub>();
            MclBnG2Mul = LazyDelegate<mclBnG2_mul>();
            MclBnG2Serialize = LazyDelegate<mclBnG2_serialize>();
            MclBnG2Deserialize = LazyDelegate<mclBnG2_deserialize>();
            MclBnG2GetStr = LazyDelegate<mclBnG2_getStr>();
            MclBnG2HashAndMapTo = LazyDelegate<mclBnG2_hashAndMapTo>();

            MclBnGtClear = LazyDelegate<mclBnGT_clear>();
            MclBnGtSetInt32 = LazyDelegate<mclBnGT_setInt32>();
            MclBnGtIsEqual = LazyDelegate<mclBnGT_isEqual>();
            MclBnGtIsZero = LazyDelegate<mclBnGT_isZero>();
            MclBnGtIsOne = LazyDelegate<mclBnGT_isOne>();
            MclBnGtNeg = LazyDelegate<mclBnGT_neg>();
            MclBnGtInv = LazyDelegate<mclBnGT_inv>();
            MclBnGtAdd = LazyDelegate<mclBnGT_add>();
            MclBnGtSub = LazyDelegate<mclBnGT_sub>();
            MclBnGtMul = LazyDelegate<mclBnGT_mul>();
            MclBnGtDiv = LazyDelegate<mclBnGT_div>();
            MclBnGtPow = LazyDelegate<mclBnGT_pow>();
            MclBnGtSerialize = LazyDelegate<mclBnGT_serialize>();
            MclBnGtDeserialize = LazyDelegate<mclBnGT_deserialize>();
            MclBnGtGetStr = LazyDelegate<mclBnGT_getStr>();

            MclBnPairing = LazyDelegate<mclBn_pairing>();
            MclBnFinalExp = LazyDelegate<mclBn_finalExp>();
            MclBnMillerLoop = LazyDelegate<mclBn_millerLoop>();
            MclBnG2LagrangeInterpolation = LazyDelegate<mclBn_G2LagrangeInterpolation>();
            MclBnG1LagrangeInterpolation = LazyDelegate<mclBn_G1LagrangeInterpolation>();
            MclBnFrLagrangeInterpolation = LazyDelegate<mclBn_FrLagrangeInterpolation>();
            MclBnFrEvaluatePolynomial = LazyDelegate<mclBn_FrEvaluatePolynomial>();
            MclBnG1EvaluatePolynomial = LazyDelegate<mclBn_G1EvaluatePolynomial>();
            MclBnG2EvaluatePolynomial = LazyDelegate<mclBn_G2EvaluatePolynomial>();
            // call init
            const int curveBls12381 = 5;
            const int compileTimeVar = 46;
            var error = MclBnInit.Value(curveBls12381, compileTimeVar);
            if (error != 0) throw new InvalidOperationException("mclBn_init returned error: " + error);
        }


        Lazy<TDelegate> LazyDelegate<TDelegate>()
        {
            var symbol = SymbolNameCache<TDelegate>.SymbolName;
            return new Lazy<TDelegate>(
                () => LoadLibNative.GetDelegate<TDelegate>(LibPtr.Value, symbol),
                true
            );
        }

        public static G2 LagrangeInterpolate(Fr[] xs, G2[] ys)
        {
            if (xs.Length != ys.Length) throw new ArgumentException("arrays are unequal length");
            unsafe
            {
                var res = new G2();
                fixed (Fr* xVec = xs)
                fixed (G2* yVec = ys)
                    Imports.MclBnG2LagrangeInterpolation.Value(&res, xVec, yVec, (ulong) xs.Length);
                return res;
            }
        }

        public static G1 LagrangeInterpolate(Fr[] xs, G1[] ys)
        {
            if (xs.Length != ys.Length) throw new ArgumentException("arrays are unequal length");
            unsafe
            {
                var res = new G1();
                fixed (Fr* xVec = xs)
                fixed (G1* yVec = ys)
                    Imports.MclBnG1LagrangeInterpolation.Value(&res, xVec, yVec, (ulong) xs.Length);
                return res;
            }
        }

        public static Fr LagrangeInterpolate(Fr[] xs, Fr[] ys)
        {
            if (xs.Length != ys.Length) throw new ArgumentException("arrays are unequal length");
            unsafe
            {
                var res = new Fr();
                fixed (Fr* xVec = xs)
                fixed (Fr* yVec = ys)
                    Imports.MclBnFrLagrangeInterpolation.Value(&res, xVec, yVec, (ulong) xs.Length);
                return res;
            }
        }

        public static Fr EvaluatePolynomial(Fr[] poly, Fr at)
        {
            unsafe
            {
                var res = new Fr();
                fixed (Fr* cVec = poly)
                    Imports.MclBnFrEvaluatePolynomial.Value(&res, cVec, (ulong) poly.Length, &at);
                return res;
            }
        }

        public static G1 EvaluatePolynomial(G1[] poly, Fr at)
        {
            unsafe
            {
                var res = new G1();
                fixed (G1* cVec = poly)
                    Imports.MclBnG1EvaluatePolynomial.Value(&res, cVec, (ulong) poly.Length, &at);
                return res;
            }
        }

        public static G2 EvaluatePolynomial(G2[] poly, Fr at)
        {
            unsafe
            {
                var res = new G2();
                fixed (G2* cVec = poly)
                    Imports.MclBnG2EvaluatePolynomial.Value(&res, cVec, (ulong) poly.Length, &at);
                return res;
            }
        }

        public static Fr[] Powers(Fr x, int n)
        {
            var result = new Fr[n];
            result[0] = Fr.One;
            for (var i = 1; i < n; ++i) result[i] = result[i - 1] * x;
            return result;
        }
    }
}