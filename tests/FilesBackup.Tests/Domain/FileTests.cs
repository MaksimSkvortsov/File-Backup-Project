using FilesBackup.Domain;
using Xunit;

namespace FilesBackup.Tests.Domain
{
    public class FileTests
    {
        [Theory]
        [InlineData("/file.txt", "file.txt")]
        [InlineData("folder/file.txt", "file.txt")]
        [InlineData("folder/file", "file")]
        public void Constructor_FilePathInput_FileNameCorrect(string path, string expectedName)
        {
            var file = new File(path, 10);

            Assert.Equal(expectedName, file.Name);
        }
    }
}
