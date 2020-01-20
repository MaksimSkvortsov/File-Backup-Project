using System;

namespace FilesBackup.Utils
{
    public class PathExtensions
    {
        public static string GetRelativePath(string fullPath, string root)
        {
            Assert.NotNull(fullPath, nameof(fullPath));
            Assert.NotNull(root, nameof(root));

            if (fullPath.Length < root.Length)
                throw new ArgumentException($"{nameof(fullPath)} cannot be shorter than {nameof(root)}");

            if(root.Length < 3)
            {
                throw new ArgumentException($"{nameof(root)} value \"{root}\" is too short. Minimum length is 3 characters");
            }

            //ToDo: consider adding root is part of fullPath verification

            if (fullPath.Length == root.Length)
                return string.Empty;

            var startIndex = root.Length == 3 ? root.Length : root.Length + 1;

            return fullPath.Substring(startIndex, fullPath.Length - startIndex);
        }
    }
}
