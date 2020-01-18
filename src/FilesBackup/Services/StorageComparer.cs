using FilesBackup.Domain;
using System.Linq;

namespace FilesBackup.Services
{
    public class StorageComparer
    {
        public StorageCompareResult Compare(StorageContent originalStorage, StorageContent destinationStorage)
        {
            var newFiles = originalStorage.Files.Except(destinationStorage.Files);
            
            //ToDo: compare files by update date

            return new StorageCompareResult(newFiles);
        }
    }
}
