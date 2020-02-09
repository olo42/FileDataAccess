// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Olo42.FileDataAccess.Contracts
{
    public interface IFileDataAccess<T>
    {
        T Read(string path);

        void Write(string path, T obj);
    }
}