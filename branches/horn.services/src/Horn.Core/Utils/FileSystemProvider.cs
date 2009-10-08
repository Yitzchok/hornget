using System;
using System.IO;
using Horn.Core.Extensions;
using Horn.Core.PackageStructure;

namespace Horn.Core.Utils
{
    public class FileSystemProvider : IFileSystemProvider
    {
        public virtual void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public virtual void CopyDirectory(string source, string destination)
        {
            Directory.Move(source, destination);
        }

        public virtual void CopyFile(string source, string destination, bool overwrite)
        {
            File.Copy(source, destination, overwrite);
        }

        public virtual DirectoryInfo CreateTemporaryHornDirectory()
        {
            var tempDirectory = new DirectoryInfo(Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "horn"));

            if(tempDirectory.Exists)
            {
                try
                {
                    tempDirectory.Delete(true);
                }
                catch(Exception ex)
                {
                    throw new CannotDeleteTempHornDirectoryException(string.Format("A problem has occurred deleting the horn directory {0}", tempDirectory.FullName), ex);
                }
            }

            tempDirectory.Create();

            return tempDirectory;
        }

        public virtual void Delete(string path)
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

        public virtual void DeleteDirectory(string path)
        {
            if (!Exists(path))
                return;

            Directory.Delete(path, true);
        }

        public virtual void DeleteFile(string path)
        {
            if (!Exists(path))
                return;

            File.Delete(path);
        }

        public virtual bool Exists(string path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }

        public DirectoryInfo GetHornRootDirectory()
        {
            var localApplicationData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            return new DirectoryInfo(Path.Combine(localApplicationData.Parent.FullName, PackageTree.RootPackageTreeName));  
        }

        public virtual void MkDir(string path)
        {
            var directoryInfo = new DirectoryInfo(path);

            directoryInfo.Create();
        }

        public virtual void MkFile(string path)
        {
            var fileInfo = new FileInfo(path);

            fileInfo.Create();
        }       

        public virtual void WriteTextFile(string destination, string text)
        {
            using(var fs = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var streamWriter = new StreamWriter(fs);
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.Write(text);
                streamWriter.Flush();
            }
        }
    }
}