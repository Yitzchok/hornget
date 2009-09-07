namespace Core.Model
{
    public class CategoryLoader : XmlToObjectLoaderBase<Category>
    {
        public CategoryLoader(string dataDirectory) : base(dataDirectory) { }
    }
}