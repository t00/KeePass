using System;
using System.IO;
using KeePassLib.Cryptography;
using Microsoft.AspNetCore.DataProtection;

namespace KeePassLib.Native
{
    public static class ProtectedData
    {
        private static readonly IDataProtector AppDataBackedProtector;

        static ProtectedData()
        {
            var appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var storageDir = Path.Combine(appDataDir, "KeePass2");
            var dataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(storageDir),  configuration => { configuration.UseEphemeralDataProtectionProvider(); });
            AppDataBackedProtector = dataProtectionProvider.CreateProtector("KeePassLib");
        }

        public static byte[] Protect(byte[] pbData, byte[] pbEnt, DataProtectionScope scope)
        {
            return AppDataBackedProtector.Protect(pbData);
        }

        public static byte[] Unprotect(byte[] pbEnc, byte[] pbEnt, DataProtectionScope currentUser)
        {
            return AppDataBackedProtector.Unprotect(pbEnc);
        }
    }
}