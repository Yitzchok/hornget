using System;
using System.IO;
using Horn.Core.extensions;

namespace Horn.Core.Utils
{
    /// <summary>
    /// Basic wrapper for the file system to aid testing.  We don't want to hit the file system in the unit tests.
    /// Keep that for the integration tests
    /// </summary>
    public interface IFileSystemProvider
    {
        void CreateDirectory(string path);

        void CopyDirectory(string source, string destination);

        void CopyFile(string source, string destination, bool overwrite);

        void DeleteDirectory(string path);

        void DeleteFile(string path);

        bool Exists(string path);

        void MkDir(string path);

        void MkFile(string path);
    }

    public class FileSystemProvider : IFileSystemProvider
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void CopyDirectory(string source, string destination)
        {
            Directory.Move(source, destination);
        }

        public void CopyFile(string source, string destination, bool overwrite)
        {
            File.Copy(source, destination, overwrite);
        }

        public void DeleteDirectory(string path)
        {
            if (!Exists(path))
                return;

            Directory.Delete(path, true);
        }

        public void DeleteFile(string path)
        {
            if (!Exists(path))
                return;

            File.Delete(path);
        }

        public void MkDir(string path)
        {
            var directoryInfo = new DirectoryInfo(path);

            directoryInfo.Create();
        }

        public void MkFile(string path)
        {
            var fileInfo = new FileInfo(path);

            fileInfo.Create();
        }

        public void Delete(string path)
        {
            var fileInfo = new FileInfo(path);

            if (fileInfo.IsFile())
            {
                fileInfo.Delete();

                return;
            }

            var directoryInfo = new DirectoryInfo(path);

            directoryInfo.Delete(true);
        }

        public bool Exists(string path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }
    }
}