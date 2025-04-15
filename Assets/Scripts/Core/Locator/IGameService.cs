using Core.Locator;
using UnityEngine;

namespace Core.Locator
{
    public interface IGameService 
    {
        void Initialize();
        bool IsInitialized { get; }
    }
}