using System.Collections.Generic;

namespace FilesBackup.Domain
{
    public class StorageContent
    {
        public StorageContent(IEnumerable<File> files)
        {
            Files = files;
        }

        public IEnumerable<File> Files { get; }
    }
}
