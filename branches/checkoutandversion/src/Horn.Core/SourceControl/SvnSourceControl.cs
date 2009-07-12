using System;
using System.IO;
using Horn.Core.PackageStructure;
using SharpSvn;

namespace Horn.Core.SCM
{
    public class SVNSourceControl : SourceControl
    {
        public override string Revision
        {
            get
            {
                SvnInfoEventArgs info = null;

                using (var client = new SvnClient())
                {
                    try
                    {
                        client.GetInfo(SvnTarget.FromUri(new Uri(Url)), out info);
                    }
                    catch (SvnRepositoryIOException sre)
                    {
                        HandleExceptions(sre);

                        throw;
                    }
                    catch (SvnObstructedUpdateException sue)
                    {
                        HandleExceptions(sue);
                    }
                }

                return info.Revision.ToString();
            }
        }

        protected override void Initialise(IPackageTree packageTree)
        {
            if(!packageTree.Root.Name.StartsWith(PackageTree.RootPackageTreeName))
                throw new InvalidOperationException("The root of the package tree is not named .horn");

            if (!packageTree.WorkingDirectory.Exists)
                return;

            try
            {
                if (packageTree.Name != PackageTree.RootPackageTreeName)
                    packageTree.WorkingDirectory.Delete(true);
            }
            catch (IOException)
            {
                throw new IOException(string.Format("The horn process is trying to delete a working directory.  Please ensure you have no applications open in the {0} directory.", packageTree.Root.CurrentDirectory.FullName));
            }
        }        

        protected override string Download(FileSystemInfo destination, GetOperation operation)
        {
            SvnUpdateResult result = null;

            using (var client = new SvnClient())
            {
                try
                {
                    switch (operation)
                    {
                        case GetOperation.CheckOut:
                            client.CheckOut(new SvnUriTarget(new Uri(Url)), destination.FullName, out result);
                            break;
                        case GetOperation.Update:
                            client.Update(destination.FullName, out result);
                            break;
                        case GetOperation.Export:
                            client.Export(Url, destination.FullName, new SvnExportArgs { Overwrite = true }, out result);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(string.Format("Unknown get operation passed to Download {0}", operation));
                    }
                        
                }
                catch (SvnRepositoryIOException sre)
                {
                    HandleExceptions(sre);

                    throw;
                }
                catch (SvnObstructedUpdateException sue)
                {
                    HandleExceptions(sue);
                }
                catch (Exception ex)
                {
                    HandleExceptions(ex);

                    throw;
                }
            }

            return result.Revision.ToString();
        }

        protected override void SetMonitor(string destination)
        {
            downloadMonitor = new DownloadMonitor(destination);
        }

        public SVNSourceControl()
        {
        }

        public SVNSourceControl(string url)
            : base(url)
        {
        }

        public SVNSourceControl(string url, string exportPath)
            : base(url, exportPath)
        {
        }
    }
}