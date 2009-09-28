using Horn.Core.Utils.IoC;

public static class IoC
{
    private static IDependencyResolver dependencyResolver;


    public static void InitializeWith(IDependencyResolver resolver)
    {
        dependencyResolver = resolver;
    }

    public static T Resolve<T>()
    {
        return dependencyResolver.Resolve<T>();
    }

    public static T Resolve<T>(string key)
    {
        return dependencyResolver.Resolve<T>(key);
    }
}