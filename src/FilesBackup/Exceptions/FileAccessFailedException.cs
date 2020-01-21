using System;

namespace FilesBackup.Exceptions
{
    public class FileAccessFailedException : Exception
    {
        public FileAccessFailedException()
        {
        }

        public FileAccessFailedException(string message) : base(message)
        {
        }

        public FileAccessFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
