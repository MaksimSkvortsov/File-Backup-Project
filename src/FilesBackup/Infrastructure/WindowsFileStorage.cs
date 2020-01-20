﻿using FilesBackup.Domain;
using FilesBackup.Exceptions;
using FilesBackup.Utils;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilesBackup.Infrastructure
{
    public class WindowsFileStorage : IStorage
    { 
        private readonly string _path; 


        public WindowsFileStorage(string path)
        {
            Assert.NotNull(path, nameof(path));
            _path = path;
        }


        public long GetAvailableSpace()
        {
            FileInfo file = new FileInfo(_path);
            DriveInfo drive = new DriveInfo(file.Directory.Root.FullName);

            if (!drive.IsReady)
            {
                throw new StorageIsNotReadyException("Drive is not ready");
            }

            try
            {
                return drive.TotalFreeSpace;
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException)
            {
                throw new StorageAccessFailedException("Drive is not availalbe", ex);
            }            
        }

        public Task<StorageContent> GetContentAsync()
        {
            return Task.Run(() =>
            {
                var files = Directory.GetFiles(_path, "*.*", SearchOption.AllDirectories);

                var fileModels = files.Select(f => ReadFileInfo(f));
                return new StorageContent(fileModels);
            });
        }

        public Stream GetFileContent(Domain.File file)
        {
            Assert.NotNull(file, nameof(file));

            var filePath = Path.Combine(_path, file.Path);
            if (!System.IO.File.Exists(filePath))
                throw new ArgumentException("File does not exist in the storage");
            return ReadFile(filePath);
        }

        private static Stream ReadFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task SaveFileAsync(Domain.File file, Stream content)
        {
            throw new NotImplementedException();
        }


        private Domain.File ReadFileInfo(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return new Domain.File(PathExtensions.GetRelativePath(filePath, _path), fileInfo.Length);
        }
    }
}
