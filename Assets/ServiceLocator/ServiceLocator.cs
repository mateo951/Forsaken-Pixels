using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        // Syntax -> scores["Alice"] = 100;
        _services[typeof(T)] = service;
        Debug.Log("Registered service " + typeof(T).Name);
    }
    
    public static void UnRegister<T>()
    {
        Debug.Log("Unregistered service " + typeof(T).Name);
        _services.Remove(typeof(T));
    }

    public static bool TryGetService<T>(out T service, bool lazyInitialize) where T : new()
    {
        if(_services.TryGetValue(typeof(T), out object value))
        {
            service = (T)value;
            return true;
        }
        // 🔥 Lazy initialization: Automatically create and register if missing and lazyInitialize is true
        if (lazyInitialize)
        {
            service = new T();
            Register(service);
            return true;
        }
        service = default(T);
        return false;
    }
}