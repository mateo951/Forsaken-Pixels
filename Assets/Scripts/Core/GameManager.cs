using UnityEngine;
using Core.Locator;
using Core.Managers;

namespace Core
{
    public class GameManager : MonoBehaviour, IGameService
    {
        public bool IsInitialized { get; private set; }

        // Manager references (assigned in Inspector)
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private HordeManager hordeManager;

        public void Initialize()
        {
            if (IsInitialized) return;

            Debug.Log("[GameManager] Initialized.");
            IsInitialized = true;
        }

        private void Awake()
        {
            // Start with a clean state
            ServiceLocator.Clear();

            // Register GameManager
            ServiceLocator.Register(this);
            ServiceLocator.Register<IGameService>(this);

            // Register other services
            ServiceLocator.Register(resourceManager);
            ServiceLocator.Register<IGameService>(resourceManager);

            ServiceLocator.Register(hordeManager);
            ServiceLocator.Register<IGameService>(hordeManager);
        }
    }
}