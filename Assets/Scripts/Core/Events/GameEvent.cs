using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Events
{
    [CreateAssetMenu(menuName = "Events/Game Event")]
    public class GameEvent : BaseScriptableObject
    {
        private readonly UnityEvent _event = new();

        public void Raise() => _event.Invoke();

        public void RegisterListener(UnityAction listener) => _event.AddListener(listener);

        public void UnregisterListener(UnityAction listener) => _event.RemoveListener(listener);
    }
}