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

        private void Start()
        {
            Clear();
        }
        
        public void SetCursor(AbstractInteractable abstractInteractable)
        {
            gameCursor.SetCursor(abstractInteractable.interactHand);
            if ((abstractInteractable.GetInteractType() & AbstractInteractable.InteractType.Inspect) != 0)
            {
                inspectIcon.SetActive(true);
                interactText.text = (abstractInteractable as ICanInspect)?.GetInspectText();
            }
        }

        public void Clear()
        {
            gameCursor.Clear();
            interactText.text = string.Empty;
            inspectIcon.gameObject.SetActive(false);
        }
    }
}
