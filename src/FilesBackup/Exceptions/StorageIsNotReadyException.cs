using System;

namespace FilesBackup.Exceptions
{
    public class StorageIsNotReadyException : Exception
    {
        public StorageIsNotReadyException()
        {
        }

        public StorageIsNotReadyException(string message) : base(message)
        {
        }
    }
}
