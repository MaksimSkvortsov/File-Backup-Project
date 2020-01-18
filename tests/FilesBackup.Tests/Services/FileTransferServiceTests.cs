using FilesBackup.Domain;
using FilesBackup.Exceptions;
using FilesBackup.Services;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace FilesBackup.Tests.Services
{
    public class FileTransferServiceTests
    {
        private FileTransferService _target;
        private FileTransferServiceTestData testData;

        public FileTransferServiceTests()
        {
            _target = new FileTransferService();
            testData = new FileTransferServiceTestData();
        }

        [Fact]
        public async Task CopyFilesAsync_MultipleFilesToTrasfer_SuccessfulResult()
        {
            var result = await _target.CopyFilesAsync(testData.OriginalStorage, testData.DestinationStorage, testData.CompareResult);

            Assert.Equal(testData.TestFiles.Count, result.Count);
            Assert.True(result.TrueForAll(x => x.TransferResult == TransferResult.Successful));
        }

        [Fact]
        public async Task CopyFilesAsync_MultipleFilesToTrasfer_VerifyStoragesCalls()
        {
            await _target.CopyFilesAsync(testData.OriginalStorage, testData.DestinationStorage, testData.CompareResult);

            var filesCount = testData.TestFiles.Count;

            testData.OriginalStorage.Received(filesCount).GetFileContent(Arg.Any<File>());
            await testData.DestinationStorage.Received(filesCount).SaveFileAsync(Arg.Any<File>(), Arg.Any<System.IO.Stream>());
        }

        [Fact]
        public async Task CopyFilesAsync_SaveOperationThrowException_FailedResult()
        {
            testData.DestinationStorage
                .SaveFileAsync(Arg.Any<File>(), Arg.Any<System.IO.Stream>())
                .Returns(x => { throw new FileSaveOperationFailedException(); });

            var result = await _target.CopyFilesAsync(testData.OriginalStorage, testData.DestinationStorage, testData.CompareResult);

            Assert.True(result.TrueForAll(x => x.TransferResult != TransferResult.Successful));
        }
    }
}
