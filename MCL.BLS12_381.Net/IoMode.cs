namespace MCL.BLS12_381.Net
{
    // Source: https://github.com/herumi/mcl/blob/master/include/mcl/op.hpp#L28
    // refer here for extended comment 
    public enum IoMode
    {
        IoBin = 2, // binary number without prefix
        IoDec = 10, // decimal number without prefix
        IoHex = 16, // hexadecimal number without prefix
        IoPrefix = 128, // append '0b'(bin) or '0x'(hex)
        IoBinPrefix = IoBin | IoPrefix,
        IoHexPrefix = IoHex | IoPrefix,
        IoEcAffine = 0, // affine coordinate
        IoEcCompY = 256, // 1-bit y representation of elliptic curve
        IoSerialize = 512, // use MBS for 1-bit y
        IoEcProj = 1024, // projective or jacobi coordinate
        IoSerializeHexStr = 2048, // printable hex string
        IoEcAffineSerialize = 4096 // serialize [x:y]
    }
}