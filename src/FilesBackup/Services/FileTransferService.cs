using FilesBackup.Domain;
using FilesBackup.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilesBackup.Services
{
    public class FileTransferService
    {
        public async Task<List<FileTransferResult>> CopyFilesAsync(IStorage source, IStorage destination, StorageCompareResult folderStructure)
        {
            Assert.NotNull(source, nameof(source));
            Assert.NotNull(destination, nameof(destination));
            Assert.NotNull(folderStructure, nameof(folderStructure));

            var result = new List<FileTransferResult>();
            foreach (var filePath in folderStructure.NewFiles)
            {
                result.Add(await TransferFileAsync(source, destination, filePath));
            }

            return result;
        }

        private async Task<FileTransferResult> TransferFileAsync(IStorage source, IStorage destination, File file)
        {
            System.IO.Stream fileContent = null;

            try
            {
                fileContent = source.GetFileContent(file);

                await destination.SaveFileAsync(file, fileContent);
                return new FileTransferResult(file);
            }
            catch(FileSaveOperationFailedException)
            {
                return new FileTransferResult(file, TransferResult.Failed);
            }
            finally
            {
                fileContent?.Dispose();
            }
        }
    }
}
