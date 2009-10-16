using System;
using Castle.Windsor;
using Horn.Core.Utils.IoC;

public static class IoC
{
    private static IDependencyResolver dependencyResolver;

    public static void AddComponentInstance<Ttype>(string key, Type service, Ttype instance)
    {
        dependencyResolver.AddComponentInstance(key, service, instance);
    }

    //TODO: Remove
    public static IWindsorContainer GetContainer()
    {
        return dependencyResolver.GetContainer();
    }

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

    public static bool RemoveComponent(string key)
    {
        return dependencyResolver.RemoveComponent(key);
    }
}