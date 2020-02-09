// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Linq;
using NUnit.Framework;
using Olo42.FileDataAccess.SerializeBinary;

namespace Olo42.FileDataAccess.Test.BinarySerializingAccess
{
  [TestFixture]
  internal class GetFilesTests
  {
    private const string testDirPath = "./testdir";
    private BinarySerializingAccess<object> dataAccess;

    [Test]
    public void GetFiles()
    {
      // Arrange
      var access = new BinarySerializingAccess<object>();

      // Act // Assert
      DirectoryInfo directoryInfo = new DirectoryInfo("./");
      Assert.That(() => this.dataAccess.GetFiles(directoryInfo), Throws.Nothing);
    }

    [Test]
    public void GetFiles_returns_directory_file()
    {
      // Arrange
      this.CreateFile("test_file_1.txt");
      this.CreateFile("test_file_2.txt");

      // Act
      var directoryInfo = new DirectoryInfo(testDirPath);
      FileInfo[] files = this.dataAccess.GetFiles(directoryInfo);

      // Assert
      Assert.That(files.First().Name, Is.EqualTo("test_file_1.txt"));
      Assert.That(files.Last().Name, Is.EqualTo("test_file_2.txt"));
    }

    [Test]
    public void GetFiles_returns_not_null()
    {
      // Arrange
      this.CreateFile("testFile");

      // Act
      var directoryInfo = new DirectoryInfo(testDirPath);
      FileInfo[] files = this.dataAccess.GetFiles(directoryInfo);

      // Assert
      Assert.That(files, Is.Not.Null);
    }

    [SetUp]
    public void SetUp()
    {
      Directory.CreateDirectory(testDirPath);
      this.dataAccess = new BinarySerializingAccess<object>();
    }

    [TearDown]
    public void TearDown()
    {
      Directory.Delete(GetFilesTests.testDirPath, true);
    }

    private void CreateFile(string fileName)
    {
      var filePath = Path.Combine(testDirPath, fileName);
      File.Create(filePath).Close();
    }
  }
}