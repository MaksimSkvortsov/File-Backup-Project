using FilesBackup.Domain;
using System;
using System.Collections.Generic;

namespace FilesBackup.Tests.Services
{
    internal class StorageComparerTestData
    {
        public StorageComparerTestData()
        {
            EmptyContent = new StorageContent(Array.Empty<File>());
            TwoFilesContent = CreateTwoFilesContent();
            FourFilesContent = CreateFourFilesContent();
        }


        public StorageContent EmptyContent { get; }
        public StorageContent TwoFilesContent { get; }
        public StorageContent FourFilesContent { get; }


        private StorageContent CreateTwoFilesContent()
        {
            var files = new List<File>
            {
                new File("/folder/file1.txt", 100),
                new File("/file1.txt", 100)
            };

            return new StorageContent(files);
        }

        private StorageContent CreateFourFilesContent()
        {
            var files = new List<File>
            {
                new File("/folder/file1.txt", 100),
                new File("/folder/file2.txt", 100),
                new File("/file1.txt", 100),
                new File("/file2.txt", 100)
            };

            return new StorageContent(files);
        }
    }
}
