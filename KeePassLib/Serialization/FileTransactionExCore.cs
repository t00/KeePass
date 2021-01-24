using System;
using System.IO;
using KeePassLib.Interfaces;

namespace KeePassLib.Serialization
{
    public class FileTransactionEx : IFileTransaction, IDisposable
    {
        public FileTransactionEx(IOConnectionInfo ioSource, bool bUseFileTransactions)
        {
            
        }

        public void Dispose()
        {
        }

        public Stream OpenWrite()
        {
            throw new NotImplementedException();
        }

        public void CommitWrite()
        {
            throw new NotImplementedException();
        }
    }
}