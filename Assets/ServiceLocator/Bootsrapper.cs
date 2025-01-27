using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ServiceLocator
{
    public static class Bootsrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            // Initialize default service locator
            ServiceLocator.Initialize();

            // Register all your services next.
            // ServiceLocator.Current.Register<IGameService>(new GameManager());
            // List of services to register
            var services = new List<IGameService>
            {
                new GameManager(),
            };
            // Register each service if it's not already registered
            foreach (var service in services)
            {
                RegisterService(service);
            }

            // Application is ready to start, load your main scene.
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        }

        private static void RegisterService<T>(T service) where T : IGameService
        {
            if (ServiceLocator.Current.Get<T>() == null)
            {
                ServiceLocator.Current.Register(service);
            }
            else
            {
                Debug.LogWarning($"{typeof(T).Name} is already registered.");
            }
        }
    }
}