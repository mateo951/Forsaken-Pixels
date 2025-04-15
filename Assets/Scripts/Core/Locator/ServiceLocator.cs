using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Locator
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            // Syntax -> scores["Alice"] = 100;
            _services[typeof(T)] = service;
            Debug.Log($"Registered service {nameof(T)}");
        }
        
        public static void UnRegister<T>()
        {
            Debug.Log("Unregistered service " + typeof(T).Name);
            _services.Remove(typeof(T));
        }
        
        // Only get registered instance	MonoBehaviours & Managers
        private static bool TryGetRegistered<T>(out T service) where T : class
        {
            if(_services.TryGetValue(typeof(T), out var value))
            {
                service = (T)value;
                return true;
            }

            service = default;
            return false;
        }
        
        // Checks registration + initialized - Safe gameplay access
        public static bool TryGetReady<T>(out T service, out string error) where T : class, IGameService
        {
            error = string.Empty;

            if (!TryGetRegistered<T>(out service))
            {
                error = $"Service of type {typeof(T).Name} is not registered.";
                return false;
            }

            if (service.IsInitialized) return true;
            error = $"Service of type {typeof(T).Name} is not initialized.";
            return false;

        }
        
        // Method that clears _services data to a clean state
        public static void Clear()
        {
            _services.Clear();
            Debug.Log("[ServiceLocator] Cleared all registered services.");
        }
    }
}