using System;

namespace KeePassLib.Utility
{
    public static class MessageService
    {
        public static string NewParagraph => Environment.NewLine + Environment.NewLine;
        
        public static string NewLine => Environment.NewLine;

        public static void ShowWarning(string strInfo, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}