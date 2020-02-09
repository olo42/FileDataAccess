// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using Olo42.FileDataAccess.Contracts;

namespace Olo42.FileDataAccess.SerializeBinary
{
  public class BinarySerializingAccess<T> : IFileDataAccess<T>
  {
    public FileInfo[] GetFiles(DirectoryInfo directoryInfo)
    {
      string[] fileNames = Directory.GetFiles(directoryInfo.FullName);

      return CreateFileInfos(fileNames);
    }

    public T Read(string path)
    {
      throw new NotImplementedException();
    }

    public void Write(string path, T obj)
    {
      throw new NotImplementedException();
    }

    private static FileInfo[] CreateFileInfos(string[] fileNames)
    {
      FileInfo[] files = new FileInfo[fileNames.Length];

      for (int i = 0; i < fileNames.Length; i++)
      {
        files[i] = new FileInfo(fileNames[i]);
      }

      return files;
    }
  }
}