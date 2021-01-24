using System.IO;
using System.Text;

namespace KeePassLib.Native
{
    public static class NativeMethods
    {
        internal const int MAX_PATH = 260;
        
        public static bool SupportsStrCmpNaturally => true;

        public static bool PathRelativePathTo(StringBuilder sb, string strBaseFile, int i, string strTargetFile, int i1)
        {
            throw new System.NotImplementedException();
        }

        public static string GetUserRuntimeDir() => Path.GetTempPath();

        public static int StrCmpNaturally(string strX, string strY)
        {
            return LexicographicStringComparer.Comparison(strX, strY);
        }
    }
}