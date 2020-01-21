using System;
using System.IO;

namespace FilesBackup.Tests
{
    class TempStorage : IDisposable
    {
        public string StoragePath { get; }


        public TempStorage()
        {
            var currentDirectory = Path.GetTempPath();
            var storageName = "FileBackupTest" + Guid.NewGuid().ToString();

            StoragePath = Path.Combine(currentDirectory, storageName);
            Directory.CreateDirectory(StoragePath);
        }


        public void AddFolder(string name)
        {
            var directoryPath = Path.Combine(StoragePath, name);

            if (Directory.Exists(directoryPath))
                throw new ArgumentException("directory already exists");

            Directory.CreateDirectory(directoryPath);
        }

        public string AddFile(string name)
        {
            var filePath = Path.Combine(StoragePath, name);

            if (File.Exists(filePath))
                throw new ArgumentException("file already exists");

            var fileDirectory = Directory.GetParent(filePath).FullName;
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

            using (var fileStream = File.Create(filePath)) { }

            return filePath;
        }

        public bool FileExists(string relativeFilePath)
        {
            return File.Exists(Path.Combine(StoragePath, relativeFilePath));
        }

        public void DeleteStorage()
        {
            if (Directory.Exists(StoragePath))
            {
                Directory.Delete(StoragePath, true);
            }
        }

        public void Dispose()
        {
            DeleteStorage();
        }
    }
}
