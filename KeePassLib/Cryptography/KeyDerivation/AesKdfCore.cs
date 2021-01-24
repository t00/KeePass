namespace KeePassLib.Cryptography.KeyDerivation
{
    public partial class AesKdf
    {
        private static bool TransformKeyGCrypt(byte[] pbNewKey, byte[] pbKeySeed32, ulong uNumRounds)
        {
            return false;
        }

        private bool TransformKeyBenchmarkGCrypt(uint uMilliseconds, out ulong uRounds)
        {
            uRounds = 0;
            return false;
        }
    }
}