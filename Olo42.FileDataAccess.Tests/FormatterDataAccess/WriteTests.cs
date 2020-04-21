// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

namespace Olo42.FileDataAccess.Test.FormatterDataAccess
{
  [TestFixture]
  internal class WriteTests
  {
    private const string TEST_DIR_PATH = "./testdir";

    private readonly string testFilePath =
      Path.Combine(TEST_DIR_PATH, "testfile.dat");

    private FileDataAccess<TestObj> dataAccess;

    [SetUp]
    public void Setup()
    {
      Directory.CreateDirectory(TEST_DIR_PATH);
      IFormatter formatter = new BinaryFormatter();
      this.dataAccess = new FileDataAccess<TestObj>(formatter);
    }

    [TearDown]
    public void TearDown()
    {
      Directory.Delete(TEST_DIR_PATH, true);
    }

    [Test]
    public void Creates_file()
    {
      // Arrange
      var testObj = new TestObj();

      // Act
      this.dataAccess.Write(this.testFilePath, testObj);

      // Assert
      Assert.That(File.Exists(this.testFilePath), Is.True, "File does not exist");
    }

    [Test]
    public void Serializes_object_to_file()
    {
      // Arrange
      var guid = Guid.NewGuid();
      var testObj = new TestObj { Identifier = guid };

      // Act
      this.dataAccess.Write(this.testFilePath, testObj);
      TestObj result = BinaryDeserialize(this.testFilePath);

      // Assert
      Assert.That(result.Identifier, Is.EqualTo(guid));
    }

    private TestObj BinaryDeserialize(string filePath)
    {
      var binaryFormatter = new BinaryFormatter();
      using (var fs = new FileStream(filePath, FileMode.Open))
      {
        return (TestObj)binaryFormatter.Deserialize(fs);
      }
    }
  }
}