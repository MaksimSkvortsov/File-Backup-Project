using System.IO;
using System.Threading.Tasks;

namespace FilesBackup.Domain
{
    public interface IStorage
    {
        Task<StorageContent> GetContentAsync();

        Stream GetFileContent(File file);

        Task SaveFileAsync(File file, Stream content);

        long GetAvailableSpace();
    }
}
