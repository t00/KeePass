using System.IO;
using System.Threading.Tasks;
using KeePassLib;
using KeePassLib.Keys;
using KeePassLib.Serialization;

namespace KeePassCore.Services
{
    public class OpenDatabaseService
    {
        private readonly LoggerService loggerService;

        public OpenDatabaseService(LoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public async Task OpenFiles(params string[] files)
        {
            foreach (var file in files)
            {
                await OpenFile(file);
            }
        }

        private async Task OpenFile(string fileName)
        {
            await using var fs = File.OpenRead(fileName);
            var database = new PwDatabase();
            database.MasterKey = new CompositeKey();
            database.MasterKey.AddUserKey(new KcpPassword("myPass"));
            var file = new KdbxFile(database);
            file.Load(fs, KdbxFormat.Default, loggerService);
        }
    }
}