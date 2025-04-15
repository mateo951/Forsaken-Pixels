using UnityEngine;
using Core.Locator;

namespace Core.Managers
{
    public class HordeManager : MonoBehaviour, IGameService
    {
        public bool IsInitialized { get; private set; }
        
        public void Initialize()
        {
            if(IsInitialized) return;
            Debug.Log("Horde Manager initialized.");
            IsInitialized = true;
        }
    }
}
