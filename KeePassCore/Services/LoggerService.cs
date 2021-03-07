using System;
using KeePassLib.Interfaces;

namespace KeePassCore.Services
{
    public class LoggerService : IStatusLogger
    {
        public void StartLogging(string strOperation, bool bWriteOperationToLog)
        {
            
        }

        public void EndLogging()
        {
            
        }

        public bool SetProgress(uint uPercent)
        {
            return true;
        }

        public bool SetText(string strNewText, LogStatusType lsType)
        {
            Console.WriteLine(strNewText);
            return true;
        }

        public bool ContinueWork()
        {
            return true;
        }
    }
}