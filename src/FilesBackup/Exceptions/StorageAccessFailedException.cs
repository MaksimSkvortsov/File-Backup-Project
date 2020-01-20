using System;

namespace FilesBackup.Exceptions
{
    public class StorageAccessFailedException : Exception
    {
        public StorageAccessFailedException()
        {
        }

        public StorageAccessFailedException(string message) : base(message)
        {
        }

        public StorageAccessFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
