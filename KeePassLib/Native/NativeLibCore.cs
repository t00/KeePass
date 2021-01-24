using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using KeePassLib.Utility;

namespace KeePassLib.Native
{
    /// <summary>
    /// Added to satisfy using namespace KeePassLib.Native
    /// </summary>
    public static class NativeLib
    {
        public static bool IsUnix() => false;// RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static int GetPlatformID() => (int)RuntimeInformation.OSArchitecture;

        public static bool TransformKey256(byte[] pbNewKey, byte[] pbKeySeed32, ulong uNumRounds)
        {
            return false;
        }
        
        public static bool TransformKeyBenchmark256(uint uMilliseconds, out ulong uRounds)
        {
            uRounds = 0;
            return false;
        }

        // Cf. DecodeArgsToData
        internal static string EncodeDataToArgs(string strData)
        {
            if(strData == null) { Debug.Assert(false); return string.Empty; }

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string str = strData;

                str = str.Replace("\\", "\\\\");
                str = str.Replace("\"", "\\\"");

                // Whether '\'' needs to be encoded depends on the context
                // (e.g. surrounding quotes); as we do not know what the
                // caller does with the returned string, we assume that
                // it will be used in a context where '\'' must not be
                // encoded; this behavior is documented
                // str = str.Replace("\'", "\\\'");

                return str;
            }

            // SHELLEXECUTEINFOW structure documentation:
            // https://docs.microsoft.com/en-us/windows/desktop/api/shellapi/ns-shellapi-shellexecuteinfow
            // return strData.Replace("\"", "\"\"\"");

            // Microsoft C/C++ startup code:
            // https://docs.microsoft.com/en-us/cpp/cpp/parsing-cpp-command-line-arguments
            // CommandLineToArgvW function:
            // https://docs.microsoft.com/en-us/windows/desktop/api/shellapi/nf-shellapi-commandlinetoargvw

            StringBuilder sb = new StringBuilder();
            int i = 0;
            while(i < strData.Length)
            {
                char ch = strData[i++];

                if(ch == '\\')
                {
                    int cBackslashes = 1;
                    while((i < strData.Length) && (strData[i] == '\\'))
                    {
                        ++cBackslashes;
                        ++i;
                    }

                    if(i == strData.Length)
                        sb.Append('\\', cBackslashes); // Assume no quote follows
                    else if(strData[i] == '\"')
                    {
                        sb.Append('\\', (cBackslashes * 2) + 1);
                        sb.Append('\"');
                        ++i;
                    }
                    else sb.Append('\\', cBackslashes);
                }
                else if(ch == '\"') sb.Append("\\\"");
                else sb.Append(ch);
            }

            return sb.ToString();
        }
        
        internal static string DecodeArgsToData(string strArgs)
        {
            if(strArgs == null) { Debug.Assert(false); return string.Empty; }

            Debug.Assert(StrUtil.Count(strArgs, "\"") == StrUtil.Count(strArgs, "\\\""));

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string str = strArgs;

                str = str.Replace("\\\"", "\"");
                str = str.Replace("\\\\", "\\");

                return str;
            }

            StringBuilder sb = new StringBuilder();
            int i = 0;
            while(i < strArgs.Length)
            {
                char ch = strArgs[i++];

                if(ch == '\\')
                {
                    int cBackslashes = 1;
                    while((i < strArgs.Length) && (strArgs[i] == '\\'))
                    {
                        ++cBackslashes;
                        ++i;
                    }

                    if(i == strArgs.Length)
                        sb.Append('\\', cBackslashes); // Assume no quote follows
                    else if(strArgs[i] == '\"')
                    {
                        Debug.Assert((cBackslashes & 1) == 1);
                        sb.Append('\\', (cBackslashes - 1) / 2);
                        sb.Append('\"');
                        ++i;
                    }
                    else sb.Append('\\', cBackslashes);
                }
                else sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}