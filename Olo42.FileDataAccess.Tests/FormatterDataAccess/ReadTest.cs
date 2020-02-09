// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using Olo42.FileDataAccess;

namespace Olo42.FileDataAccess.Test.FormatterDataAccess
{
  [TestFixture]
  internal class ReadTest
  {
    private const string TEST_DIR_PATH = "./testdir";
    private readonly string testFilePath =
      Path.Combine(TEST_DIR_PATH, "testfile.dat");
    private FormatterDataAccess<TestObj> dataAccess;

    [Test]
    public void Deserializes_object()
    {
      // Arrange
      this.BinarySerialize(new TestObj());
      // Act
      object readObject = this.dataAccess.Read(this.testFilePath);

      // Assert
      Assert.That(readObject, Is.Not.Null);
    }

    [Test]
    public void Deserializes_same_object()
    {
      // Arrange
      var guid = Guid.NewGuid();
      var obj = new TestObj { Identifier = guid };
      this.BinarySerialize(obj);

      // Act
      TestObj readObject = this.dataAccess.Read(testFilePath);

      // Assert
      Assert.That(readObject.Identifier, Is.EqualTo(guid));
    }

    [SetUp]
    public void Setup()
    {
      Directory.CreateDirectory(TEST_DIR_PATH);
      IFormatter formatter = new BinaryFormatter();
      this.dataAccess = new FormatterDataAccess<TestObj>(formatter);
    }

    [TearDown]
    public void TearDown()
    {
      Directory.Delete(TEST_DIR_PATH, true);
    }

    private void BinarySerialize(TestObj obj)
    {
      using (var fs = new FileStream(this.testFilePath, FileMode.Create))
      {
        var formatter = new BinaryFormatter();
        formatter.Serialize(fs, obj);
      }
    }
  }
}