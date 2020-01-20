using FilesBackup.Utils;
using System;
using Xunit;

namespace FilesBackup.Tests.Utils
{
    public class PathExtensionsTests
    {
        [Theory]
        [InlineData(@"C:\Folder\Inner folder", @"C:\Folder\Inner folder", "")]
        [InlineData(@"C:\Folder\Inner folder\file.txt", @"C:\Folder", @"Inner folder\file.txt")]
        [InlineData(@"C:\Folder\Inner folder\file.txt", @"C:\Folder\Inner folder", @"file.txt")]
        [InlineData(@"C:\Folder\Inner folder\file.txt", @"C:\", @"Folder\Inner folder\file.txt")]
        public void GetRelativePath_ValidInput(string fullPath, string root, string expectedRelativePath)
        {
            var result = PathExtensions.GetRelativePath(fullPath, root);

            Assert.Equal(expectedRelativePath, result);
        }

        [Theory]
        [InlineData(@"C:\Folder\Inner folder\file.txt", @"C:")]
        [InlineData(@"C:\Folder\Inner folder", @"C:\Folder\Inner folder\file.txt")]
        public void GetRelativePath_InvalidInput(string fullPath, string root)
        {
            Assert.Throws<ArgumentException>(() => PathExtensions.GetRelativePath(fullPath, root));
        }
    }
}
