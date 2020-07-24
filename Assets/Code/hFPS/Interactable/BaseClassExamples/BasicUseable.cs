using UnityEngine.Events;

namespace hFPS.Interactable.BaseClassExamples
{
    public class BasicUseable : AbstractInteractable, ICanBeUsed
    {
        public UnityEvent OnUse;
        
        public string useText = "use";
        
        public string GetUseText() => useText;
        public void DoUse()
        {
            OnUse?.Invoke();
        }
    }
}