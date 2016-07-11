namespace RoyalSoft.Network.Core.Compression
{
    interface ICompressor
    {
        byte[] Compress(byte[] data);
        byte[] Decompress(byte[] data);
    }
}
