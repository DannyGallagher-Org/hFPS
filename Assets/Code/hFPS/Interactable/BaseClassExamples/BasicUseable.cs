using System;
using hFPS.Bindings;
using UnityEngine.Events;

namespace hFPS.Interactable.BaseClassExamples
{
    public class BasicUseable : AbstractInteractable, ICanBeUsed
    {
        private PlayerActions _playerActions;
        
        public UnityEvent onUse;
        public UnityEvent onUnuse;
        
        public string useText = "use";
        
        public string GetUseText() => useText;

        public virtual void DoUse()
        {
            onUse?.Invoke();
        }

        public virtual void DoUnuse()
        {
            onUnuse?.Invoke();
        }
    }
}