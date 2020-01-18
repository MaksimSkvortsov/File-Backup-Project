using FilesBackup.Domain;
using FilesBackup.Services;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilesBackup.Tests.Services
{
    internal class FileTransferServiceTestData
    {
        public FileTransferServiceTestData()
        {
            OriginalStorage.GetFileContent(Arg.Any<File>()).Returns(new System.IO.MemoryStream());
            DestinationStorage.SaveFileAsync(Arg.Any<File>(), Arg.Any<System.IO.MemoryStream>()).Returns(Task.CompletedTask);

            TestFiles = new List<File>
            {
                new File("/folder/file1.txt", 100),
                new File("/file1.txt", 100)
            };

            CompareResult = new StorageCompareResult(TestFiles);
        }

        public IStorage OriginalStorage = Substitute.For<IStorage>();
        public IStorage DestinationStorage = Substitute.For<IStorage>();

        public IReadOnlyCollection<File> TestFiles { get; }
        public StorageCompareResult CompareResult { get; }
    }
}
