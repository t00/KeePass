using System.Security.Cryptography;
using KeePassLib.Cryptography;

namespace KeePassLib.Native
{
    public static class ProtectedMemory
    {
        public static void Protect(byte[] data, MemoryProtectionScope scope)
        {
            CryptoUtil.ProtectData(data, null, DataProtectionScope.CurrentUser);
        }

        public static void Unprotect(byte[] data, MemoryProtectionScope scope)
        {
            CryptoUtil.UnprotectData(data, null, DataProtectionScope.CurrentUser);
        }
    }
}