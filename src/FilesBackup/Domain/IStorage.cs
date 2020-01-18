using System.IO;
using System.Threading.Tasks;

namespace FilesBackup.Domain
{
    public interface IStorage
    {
        Task<StorageContent> GetContent();

        Stream GetFile(File file);

        Task SaveFileAsync(File file, Stream stream);

        long GetAvailableSpace();
    }
}
