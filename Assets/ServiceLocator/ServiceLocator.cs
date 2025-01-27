using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator
{
    public class ServiceLocator
    {
        /// Private constructor 
        private ServiceLocator()
        {
        }
        
        /// Current registered _services 
        private readonly Dictionary<string, IGameService> _services = new Dictionary<string, IGameService>();

        /// Currently active service locator instance 
        public static ServiceLocator Current { get; private set; }

        /// Initializes the service locator with a new instance.
        public static void Initialize()
        {
            Current = new ServiceLocator();
        }

        /// Gets the service instance of the given type.
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance.</returns>
        public T Get<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
            }
            return (T)_services[key];
        }

        /// Registers the service with the current service locator.
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public void Register<T>(T service) where T : IGameService
        {
            string key = typeof(T).Name;
            Debug.Log(key);
            if (_services.ContainsKey(key))
            {
                Debug.LogError(
                    $"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }
            _services.Add(key, service);
        }

        /// Unregisters the service from the current service locator.
        /// <typeparam name="T">Service type.</typeparam>
        public void Unregister<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError(
                    $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }
            _services.Remove(key);
        }
    }
}