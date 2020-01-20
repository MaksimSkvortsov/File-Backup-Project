using FilesBackup.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FilesBackup.Tests.Infrastructure
{
    public class WindowsFileStorageTests
    {
        private WindowsFileStorage _target;
        private TempStorage originalStorage;

        public WindowsFileStorageTests()
        {
            originalStorage = new TempStorage();
            _target = new WindowsFileStorage(originalStorage.StoragePath);
        }


        [Fact]
        public void GetAvailableSpace_StorageExists_NotZero()
        {
            var space = _target.GetAvailableSpace();

            Assert.True(space > 0);
        }


        [Fact]
        public async Task GetContentAsync_StorageIsEmpty_ContentIsValid()
        {
            var files = new List<string> { };
            files.ForEach(f => originalStorage.AddFile(f));

            var content = await _target.GetContentAsync();

            Assert.Empty(content.Files);
        }

        [Fact]
        public async Task GetContentAsync_StorageHasFiles_ContentIsValid()
        {
            var files = new List<string> { "file 1.txt", "file 2", "folder\\file 3.txt" };
            files.ForEach(f => originalStorage.AddFile(f));

            var content = await _target.GetContentAsync();

            Assert.Equal(files.Count, content.Files.Count());
            Assert.Equal(files, content.Files.Select(f => f.Path));
        }
    }
}
