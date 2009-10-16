namespace Horn.Core.Utils.IoC
{
    public interface IDependencyResolver
    {
        T Resolve<T>();

        T Resolve<T>(string key);

        void AddComponentInstance<T>(T component);
    }
}