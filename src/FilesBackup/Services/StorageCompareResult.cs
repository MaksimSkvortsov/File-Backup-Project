using FilesBackup.Domain;
using System.Collections.Generic;

namespace FilesBackup.Services
{
    public class StorageCompareResult
    {
        public StorageCompareResult(IEnumerable<File> newFiles)
        {
            Assert.NotNull(newFiles, nameof(newFiles));

            NewFiles = newFiles;
        }

        public IEnumerable<File> NewFiles { get; }
    }
}
