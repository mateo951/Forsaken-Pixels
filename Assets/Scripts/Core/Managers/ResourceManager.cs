using UnityEngine;
using Core.Locator;

namespace Core.Managers
{
    public class ResourceManager : MonoBehaviour, IGameService
    {
        public bool IsInitialized { get; private set; }
        public void Initialize()
        {
            if (IsInitialized) return;
            Debug.Log("Resource Manager initialized.");
            IsInitialized = true;
        }
    }
}

