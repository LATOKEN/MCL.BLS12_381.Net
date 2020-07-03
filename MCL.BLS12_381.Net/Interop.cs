// ReSharper disable InconsistentNaming

namespace MCL.BLS12_381.Net
{
    [SymbolName(nameof(mclBn_init))]
    public delegate int mclBn_init(int curve, int maxUnitSize);

    /* ====== Fr ====== */
    
    [SymbolName(nameof(mclBnFr_clear))]
    public unsafe delegate void mclBnFr_clear(Fr* x);

    [SymbolName(nameof(mclBnFr_setInt32))]
    public unsafe delegate void mclBnFr_setInt32(Fr* y, int x);

    [SymbolName(nameof(mclBnFr_setByCSPRNG))]
    public unsafe delegate int mclBnFr_setByCSPRNG(Fr* y);

    [SymbolName(nameof(mclBnFr_setStr))]
    public unsafe delegate int mclBnFr_setStr(Fr* x, void* input, ulong bufSize, int ioMode);

    [SymbolName(nameof(mclBnFr_getStr))]
    public unsafe delegate ulong mclBnFr_getStr(void* output, ulong maxBufSize, Fr* x, int ioMode);

    [SymbolName(nameof(mclBnFr_isValid))]
    public unsafe delegate int mclBnFr_isValid(Fr* x);

    [SymbolName(nameof(mclBnFr_isEqual))]
    public unsafe delegate int mclBnFr_isEqual(Fr* x, Fr* y);

    [SymbolName(nameof(mclBnFr_isZero))]
    public unsafe delegate int mclBnFr_isZero(Fr* x);

    [SymbolName(nameof(mclBnFr_isOne))]
    public unsafe delegate int mclBnFr_isOne(Fr* x);

    [SymbolName(nameof(mclBnFr_neg))]
    public unsafe delegate void mclBnFr_neg(Fr* y, Fr* x);

    [SymbolName(nameof(mclBnFr_inv))]
    public unsafe delegate void mclBnFr_inv(Fr* y, Fr* x);
    
    [SymbolName(nameof(mclBnFr_sqr))]
    public unsafe delegate void mclBnFr_sqr(Fr* y, Fr* x);

    [SymbolName(nameof(mclBnFr_add))]
    public unsafe delegate void mclBnFr_add(Fr* z, Fr* x, Fr* y);

    [SymbolName(nameof(mclBnFr_sub))]
    public unsafe delegate void mclBnFr_sub(Fr* z, Fr* x, Fr* y);

    [SymbolName(nameof(mclBnFr_mul))]
    public unsafe delegate void mclBnFr_mul(Fr* z, Fr* x, Fr* y);

    [SymbolName(nameof(mclBnFr_div))]
    public unsafe delegate void mclBnFr_div(Fr* z, Fr* x, Fr* y);

    [SymbolName(nameof(mclBnFr_serialize))]
    public unsafe delegate ulong mclBnFr_serialize(void* output, ulong maxBufSize, Fr* x);

    [SymbolName(nameof(mclBnFr_deserialize))]
    public unsafe delegate ulong mclBnFr_deserialize(Fr* x, void* input, ulong bufSize);

    [SymbolName(nameof(mclBnG1_getStr))]
    public unsafe delegate ulong mclBnG1_getStr(void* output, ulong maxBufSize, G1* x, int ioMode);

    /* ====== G1 ====== */
    
    [SymbolName(nameof(mclBnG1_clear))]
    public unsafe delegate void mclBnG1_clear(G1* x);

    [SymbolName(nameof(mclBnG1_isValid))]
    public unsafe delegate int mclBnG1_isValid(G1* x);

    [SymbolName(nameof(mclBnG1_isEqual))]
    public unsafe delegate int mclBnG1_isEqual(G1* x, G1* y);

    [SymbolName(nameof(mclBnG1_isZero))]
    public unsafe delegate int mclBnG1_isZero(G1* x);

    [SymbolName(nameof(mclBnG1_neg))]
    public unsafe delegate void mclBnG1_neg(G1* y, G1* x);

    [SymbolName(nameof(mclBnG1_dbl))]
    public unsafe delegate void mclBnG1_dbl(G1* y, G1* x);

    [SymbolName(nameof(mclBnG1_add))]
    public unsafe delegate void mclBnG1_add(G1* z, G1* x, G1* y);

    [SymbolName(nameof(mclBnG1_sub))]
    public unsafe delegate void mclBnG1_sub(G1* z, G1* x, G1* y);

    [SymbolName(nameof(mclBnG1_mul))]
    public unsafe delegate void mclBnG1_mul(G1* z, G1* x, Fr* y);

    [SymbolName(nameof(mclBnG1_serialize))]
    public unsafe delegate ulong mclBnG1_serialize(void* output, ulong maxBufSize, G1* x);

    [SymbolName(nameof(mclBnG1_deserialize))]
    public unsafe delegate ulong mclBnG1_deserialize(G1* x, void* input, ulong bufSize);
    
    [SymbolName(nameof(mclBnG1_hashAndMapTo))]
    public unsafe delegate void mclBnG1_hashAndMapTo(G1* z, void* input, ulong bufSize);

    /* ====== G2 ====== */
    
    [SymbolName(nameof(mclBnG2_clear))]
    public unsafe delegate void mclBnG2_clear(void* x);

    [SymbolName(nameof(mclBnG2_isValid))]
    public unsafe delegate int mclBnG2_isValid(void* x);

    [SymbolName(nameof(mclBnG2_isEqual))]
    public unsafe delegate int mclBnG2_isEqual(void* x, void* y);

    [SymbolName(nameof(mclBnG2_isZero))]
    public unsafe delegate int mclBnG2_isZero(void* x);

    [SymbolName(nameof(mclBnG2_neg))]
    public unsafe delegate void mclBnG2_neg(void* y, void* x);

    [SymbolName(nameof(mclBnG2_dbl))]
    public unsafe delegate void mclBnG2_dbl(void* y, void* x);

    [SymbolName(nameof(mclBnG2_add))]
    public unsafe delegate void mclBnG2_add(void* z, void* x, void* y);

    [SymbolName(nameof(mclBnG2_sub))]
    public unsafe delegate void mclBnG2_sub(void* z, void* x, void* y);

    [SymbolName(nameof(mclBnG2_mul))]
    public unsafe delegate void mclBnG2_mul(void* z, void* x, void* y);

    [SymbolName(nameof(mclBnG2_hashAndMapTo))]
    public unsafe delegate void mclBnG2_hashAndMapTo(void* z, void* input, ulong bufSize);

    [SymbolName(nameof(mclBnG2_getStr))]
    public unsafe delegate ulong mclBnG2_getStr(void* output, ulong maxBufSize, void* x, int ioMode);

    [SymbolName(nameof(mclBnG2_setStr))]
    public unsafe delegate int mclBnG2_setStr(void* x, void* input, ulong bufSize, int ioMode);

    [SymbolName(nameof(mclBnG2_serialize))]
    public unsafe delegate ulong mclBnG2_serialize(void* output, ulong maxBufSize, void* g2);

    [SymbolName(nameof(mclBnG2_deserialize))]
    public unsafe delegate ulong mclBnG2_deserialize(void* x, void* buf, ulong bufSize);

    /* ====== GT ====== */
    
    [SymbolName(nameof(mclBnGT_clear))]
    public unsafe delegate void mclBnGT_clear(GT* x);
    
    [SymbolName(nameof(mclBnGT_setInt32))]
    public unsafe delegate void mclBnGT_setInt32(GT* y, int x);

    [SymbolName(nameof(mclBnGT_isEqual))]
    public unsafe delegate int mclBnGT_isEqual(GT* x, GT* y);

    [SymbolName(nameof(mclBnGT_isZero))]
    public unsafe delegate int mclBnGT_isZero(GT* x);

    [SymbolName(nameof(mclBnGT_isOne))]
    public unsafe delegate int mclBnGT_isOne(GT* x);

    [SymbolName(nameof(mclBnGT_neg))]
    public unsafe delegate void mclBnGT_neg(GT* y, GT* x);

    [SymbolName(nameof(mclBnGT_inv))]
    public unsafe delegate void mclBnGT_inv(GT* y, GT* x);

    [SymbolName(nameof(mclBnGT_add))]
    public unsafe delegate void mclBnGT_add(GT* z, GT* x, GT* y);

    [SymbolName(nameof(mclBnGT_sub))]
    public unsafe delegate void mclBnGT_sub(GT* z, GT* x, GT* y);

    [SymbolName(nameof(mclBnGT_mul))]
    public unsafe delegate void mclBnGT_mul(GT* z, GT* x, GT* y);

    [SymbolName(nameof(mclBnGT_div))]
    public unsafe delegate void mclBnGT_div(GT* z, GT* x, GT* y);

    [SymbolName(nameof(mclBnGT_pow))]
    public unsafe delegate void mclBnGT_pow(GT* z, GT* x, Fr* y);
    
    [SymbolName(nameof(mclBnGT_serialize))]
    public unsafe delegate ulong mclBnGT_serialize(void* output, ulong maxBufSize, GT* x);
    
    [SymbolName(nameof(mclBnGT_deserialize))]
    public unsafe delegate ulong mclBnGT_deserialize(GT *z, void* input, ulong maxBufSize);

    [SymbolName(nameof(mclBnGT_getStr))]
    public unsafe delegate ulong mclBnGT_getStr(void* output, ulong maxBufSize, GT* x, int ioMode);
    
    /* ====== Operations ====== */

    [SymbolName(nameof(mclBn_pairing))]
    public unsafe delegate void mclBn_pairing(void* z, void* x, void* y);

    [SymbolName(nameof(mclBn_finalExp))]
    public unsafe delegate void mclBn_finalExp(void* y, void* x);

    [SymbolName(nameof(mclBn_millerLoop))]
    public unsafe delegate void mclBn_millerLoop(void* z, void* x, void* y);

    [SymbolName(nameof(mclBn_G2LagrangeInterpolation))]
    public unsafe delegate int mclBn_G2LagrangeInterpolation(G2* res, Fr* xVec, G2* yVec, ulong k);

    [SymbolName(nameof(mclBn_G1LagrangeInterpolation))]
    public unsafe delegate int mclBn_G1LagrangeInterpolation(G1* res, Fr* xVec, G1* yVec, ulong k);
    
    [SymbolName(nameof(mclBn_FrLagrangeInterpolation))]
    public unsafe delegate int mclBn_FrLagrangeInterpolation(Fr* res, Fr* xVec, Fr* yVec, ulong k);

    [SymbolName(nameof(mclBn_FrEvaluatePolynomial))]
    public unsafe delegate int mclBn_FrEvaluatePolynomial(Fr* res, Fr* cVec, ulong k, Fr* at);

    [SymbolName(nameof(mclBn_G1EvaluatePolynomial))]
    public unsafe delegate int mclBn_G1EvaluatePolynomial(G1* res, G1* cVec, ulong k, Fr* at);

    [SymbolName(nameof(mclBn_G2EvaluatePolynomial))]
    public unsafe delegate int mclBn_G2EvaluatePolynomial(G2* res, G2* cVec, ulong k, Fr* at);
}