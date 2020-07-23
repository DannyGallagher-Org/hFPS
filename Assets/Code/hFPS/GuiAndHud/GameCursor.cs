using System;
using hFPS.Interactable;
using UnityEngine;

namespace hFPS.GuiAndHud
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameCursor : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private BasicGuiItem outerRim;
        [SerializeField] private BasicGuiItem leftInner;
        [SerializeField] private BasicGuiItem rightInner;

        [Header("Settings")] 
        public float showDuration = 0.5f;
        public float hideDuration = 0.3f;

        public void SetCursor(AbstractInteractable.InteractHand hand)
        {
            switch (hand)
            {
                case AbstractInteractable.InteractHand.LeftHand:
                    leftInner.Show(showDuration);
                    rightInner.Hide(hideDuration);
                    break;
                case AbstractInteractable.InteractHand.RightHand:
                    rightInner.Show(showDuration);
                    leftInner.Hide(hideDuration);
                    break;
                case AbstractInteractable.InteractHand.BothHands:
                    leftInner.Show(showDuration);
                    rightInner.Show(showDuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hand), hand, null);
            }
        }
        
        public void Clear()
        {
            leftInner.Hide(hideDuration);
            rightInner.Hide(hideDuration);
        }
    }
}
