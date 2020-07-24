using hFPS.Interactable;
using TMPro;
using UnityEngine;

namespace hFPS.GuiAndHud
{
    public class HudManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameCursor gameCursor;
        [SerializeField] private TextMeshProUGUI interactText;

        [Header("Icons")] 
        [SerializeField] private GameObject inspectIcon;
        [SerializeField] private GameObject useIcon;
        [SerializeField] private GameObject grabIcon;

        private void Start()
        {
            Clear();
        }
        
        public void SetCursor(AbstractInteractable abstractInteractable)
        {
            gameCursor.SetCursor(abstractInteractable.interactHand);
            var firstText = string.Empty;
            if ((abstractInteractable.GetInteractType() & AbstractInteractable.InteractType.Inspect) != 0)
            {
                inspectIcon.SetActive(true);
                if(string.IsNullOrEmpty(firstText))
                    firstText = (abstractInteractable as ICanInspect)?.GetInspectText();
            }
            
            if ((abstractInteractable.GetInteractType() & AbstractInteractable.InteractType.Use) != 0)
            {
                useIcon.SetActive(true);
                if(string.IsNullOrEmpty(firstText))
                    firstText = (abstractInteractable as ICanBeUsed)?.GetUseText();
            }
            
            if ((abstractInteractable.GetInteractType() & AbstractInteractable.InteractType.Grab) != 0)
            {
                grabIcon.SetActive(true);
                if(string.IsNullOrEmpty(firstText))
                firstText = (abstractInteractable as ICanBeGrabbed)?.GetGrabText();
            }
            
            interactText.text = firstText;
        }

        public void Clear()
        {
            gameCursor.Clear();
            interactText.text = string.Empty;
            inspectIcon.gameObject.SetActive(false);
            useIcon.gameObject.SetActive(false);
            grabIcon.gameObject.SetActive(false);
        }
    }
}
