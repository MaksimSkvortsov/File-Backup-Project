using FilesBackup.Domain;
using FilesBackup.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FilesBackup.Tests.Services
{
    public class StorageComparerTests
    {
        private StorageComparerTestData _testData = new StorageComparerTestData();
        private StorageComparer _target = new StorageComparer();

        [Fact]
        public void Compare_EqualStorages_EmptyResult()
        {
            var source = _testData.TwoFilesContent;
            var destination = _testData.TwoFilesContent;

            var result = _target.Compare(source, destination);

            Assert.Empty(result.NewFiles);
        }

        [Fact]
        public void Compare_SourceStoragesHasNewFiles_NewFilesResult()
        {
            var source = _testData.FourFilesContent;
            var destination = _testData.TwoFilesContent;

            var result = _target.Compare(source, destination);

            Assert.Equal(2, result.NewFiles.Count());
        }
    }
}
