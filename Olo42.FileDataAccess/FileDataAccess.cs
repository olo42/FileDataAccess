// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Runtime.Serialization;
using Olo42.FileDataAccess.Contracts;

namespace Olo42.FileDataAccess
{
  public class FileDataAccess<T> : IFileDataAccess<T>
  {
    private readonly IFormatter formatter;

    public FileDataAccess(IFormatter formatter)
    {
      this.formatter = formatter;
    }

    public FileInfo[] GetFiles(DirectoryInfo directoryInfo)
    {
      string[] fileNames = Directory.GetFiles(directoryInfo.FullName);

      return CreateFileInfos(fileNames);
    }

    public T Read(string path)
    {
      using (var fs = new FileStream(path, FileMode.Open))
      {
        return (T)this.formatter.Deserialize(fs);
      }
    }

    public void Write(string path, T obj)
    {
      using (var fs = new FileStream(path, FileMode.OpenOrCreate))
      {
        this.formatter.Serialize(fs, obj);
      }
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