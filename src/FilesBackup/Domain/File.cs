using System.Collections.Generic;

namespace FilesBackup.Domain
{
    public class File
    {
        public File(string path, long size)
        {
            Assert.NotNull(path, nameof(path));

            Path = path;
            Size = size;
            Name = System.IO.Path.GetFileName(path);
        }

        public string Path { get; }

        public string Name { get; }

        public long Size { get; }


        #region override

        public override bool Equals(object obj)
        {
            return obj is File file &&
                   Path == file.Path &&
                   Size == file.Size;
        }

        public override int GetHashCode()
        {
            var hashCode = -1038045630;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Path);
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            return hashCode;
        }

        #endregion
    }
}
