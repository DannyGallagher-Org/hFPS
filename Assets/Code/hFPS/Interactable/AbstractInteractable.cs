using System;
using UnityEngine;

namespace hFPS.Interactable
{
    public abstract class AbstractInteractable : MonoBehaviour
    {
        public enum InteractHand
        {
            LeftHand,
            RightHand,
            BothHands
        }
        
        [Flags]
        public enum InteractType
        {
            Inspect = 1 << 0,
            Use    = 1 << 1,
            Search = 1 << 2,
            PickUp = 1 << 3,
            
            Count
        }

        public InteractHand interactHand = InteractHand.LeftHand;

        public InteractType GetInteractType()
        {
            InteractType result = 0;
            if (this is ICanInspect)
                result |= InteractType.Inspect;

            if (this is ICanBeUsed)
                result |= InteractType.Use;

            return result;
        }
    }
}
