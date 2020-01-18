using System;

namespace FilesBackup.Exceptions
{
    public class FileSaveOperationFailedException : Exception
    {
        public FileSaveOperationFailedException()
        {
        }

        public FileSaveOperationFailedException(string message) : base(message)
        {
        }

        public FileSaveOperationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
