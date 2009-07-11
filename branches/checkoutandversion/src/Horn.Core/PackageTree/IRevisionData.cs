namespace Horn.Core.PackageStructure
{
    public interface IRevisionData
    {
        void RecordRevision(IPackageTree packageTree, string revisionVlaue);

        string Revision { get; }

        bool ShouldCheckOut();

        bool ShouldUpdate(IRevisionData other);
    }
}