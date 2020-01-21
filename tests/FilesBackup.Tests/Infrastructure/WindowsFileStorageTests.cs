using FilesBackup.Exceptions;
using FilesBackup.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Fact]
        public void GetFileContent_FileExists_CanRead()
        {
            var filePath = "folder\\file 3.txt";
            originalStorage.AddFile(filePath);

            using (var fileContent = _target.GetFileContent(new FilesBackup.Domain.File(filePath, 0)))
            {
                Assert.True(fileContent.CanRead);
            }
        }

        [Fact]
        public void GetFileContent_FileDoNotExists_Exception()
        {
            var filePath = "does not exist.txt";

            Assert.Throws<ArgumentException>(() => _target.GetFileContent(new FilesBackup.Domain.File(filePath, 0)));
        }

        [Fact]
        public async Task SaveFileAsync_FileExists_CanWriteAndRead()
        {
            var filePath = "folder\\file 3.txt";
            var newFilePath = "folder\\file 4.txt";

            var existingFileAbsolutePath = originalStorage.AddFile(filePath);

            using (var stream = new FileStream(existingFileAbsolutePath, FileMode.Open))
            {
                var newFile = new FilesBackup.Domain.File(newFilePath, stream.Length);
                await _target.SaveFileAsync(newFile, stream);
            }

            using (var fileContent = _target.GetFileContent(new FilesBackup.Domain.File(filePath, 0)))
            {
                Assert.True(fileContent.CanRead);
            }
        }

        [Fact]
        public async Task SaveFileAsync_FileDoNotExist_CanWriteAndRead()
        {
            var filePath = "folder\\file 3.txt";
            var newFilePath = "folder\\file 4.txt";

            var existingFileAbsolutePath = originalStorage.AddFile(filePath);

            var stream = new FileStream(existingFileAbsolutePath, FileMode.Open);
            stream.Close();

            var newFile = new FilesBackup.Domain.File(newFilePath, 0);
            await Assert.ThrowsAsync<FileAccessFailedException>(() => _target.SaveFileAsync(newFile, stream));
        }
    }
}
