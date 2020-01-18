using FilesBackup.Domain;

namespace FilesBackup.Services
{
    public enum TransferResult
    {
        Successful,
        Failed //ToDo: add more detailed failures
    }

    public class FileTransferResult
    {
        public FileTransferResult(File file, TransferResult transferResult = TransferResult.Successful)
        {
            File = file;
            TransferResult = transferResult;
        }


        public File File { get; }

        public TransferResult TransferResult { get; }
    }
}