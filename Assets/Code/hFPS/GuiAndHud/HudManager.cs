using System.Collections.Generic;
using DG.Tweening;
using hFPS.Interactable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace hFPS.GuiAndHud
{
    public class HudManager : MonoBehaviour
    {
        public enum HudInteractSetting
        {
            Inspect,
            Use,
            Grab
        }

        [Header("CanvasGroups")] 
        [SerializeField] private CanvasGroup cursorCanvasGroup;
        [SerializeField] private CanvasGroup interactablesCanvasGroup;
        
        [Header("Components")]
        [SerializeField] private GameCursor gameCursor;
        [SerializeField] private TextMeshProUGUI interactText;

        [Header("Icons")] 
        [SerializeField] private Image inspectIcon;
        [SerializeField] private Image useIcon;
        [SerializeField] private Image grabIcon;

        private int _currentActive;
        private List<Image> _currentImages = new List<Image>();
        private List<string> _currentTexts = new List<string>();
        private List<HudInteractSetting> _currentSettings = new List<HudInteractSetting>();
        
        private GameObject _currentInspectText;
        private bool _inspecting;

        public HudInteractSetting CurrentSetting => _currentSettings[_currentActive];

        private void Start()
        {
            Clear();
        }

        public void Hide()
        {
            cursorCanvasGroup.DOFade(0f, 0.2f);
            interactablesCanvasGroup.DOFade(0f, 0.2f);
        }

        public void Show()
        {
            cursorCanvasGroup.DOFade(1f, 0.2f);
            interactablesCanvasGroup.DOFade(1f, 0.2f);
        }

        public void ScrollUp()
        {
            if (_currentImages.Count <= 0 || _currentActive <= 0) 
                return;
            
            _currentImages[_currentActive].CrossFadeAlpha(0.2f, 0.3f, false);
            _currentActive--;
            _currentImages[_currentActive].CrossFadeAlpha(1f, 0.3f, false);
            interactText.text = _currentTexts[_currentActive];
        }
        
        public void ScrollDown()
        {
            if (_currentImages.Count <= 0 || _currentActive >= _currentImages.Count - 1) 
                return;
            
            _currentImages[_currentActive].CrossFadeAlpha(0.2f, 0.3f, false);
            _currentActive++;
            _currentImages[_currentActive].CrossFadeAlpha(1f, 0.3f, false);
            interactText.text = _currentTexts[_currentActive];
        }
        
        public void SetCursor(AbstractInteractable abstractInteractable)
        {
            Clear();
            gameCursor.SetCursor(abstractInteractable.interactHand);
            var firstText = string.Empty;
            
            if ((abstractInteractable.GetInteractType() & AbstractInteractable.InteractType.Inspect) != 0)
            {
                inspectIcon.gameObject.SetActive(true);
                _currentImages.Add(inspectIcon);
                _currentSettings.Add(HudInteractSetting.Inspect);
                var inspectText = (abstractInteractable as ICanInspect)?.GetInspectText();
                _currentTexts.Add(inspectText);
                if (string.IsNullOrEmpty(firstText))
                {
                    firstText = inspectText;
                    inspectIcon.CrossFadeAlpha(1f, 0.3f, false);
                }
                else
                    inspectIcon.CrossFadeAlpha(0.2f, 0.3f, false);
            }
            else
                inspectIcon.gameObject.SetActive(false);
            
            if ((abstractInteractable.GetInteractType() & AbstractInteractable.InteractType.Use) != 0)
            {
                useIcon.gameObject.SetActive(true);
                _currentImages.Add(useIcon);
                _currentSettings.Add(HudInteractSetting.Use);
                var useText = (abstractInteractable as ICanBeUsed)?.GetUseText();
                _currentTexts.Add(useText);
                if (string.IsNullOrEmpty(firstText))
                {
                    firstText = useText;
                    useIcon.CrossFadeAlpha(1f, 0.3f, false);
                }
                else
                    useIcon.CrossFadeAlpha(0.2f, 0.3f, false);
            }
            else
                useIcon.gameObject.SetActive(false);
            
            if ((abstractInteractable.GetInteractType() & AbstractInteractable.InteractType.Grab) != 0)
            {
                grabIcon.gameObject.SetActive(true);
                _currentImages.Add(grabIcon);
                _currentSettings.Add(HudInteractSetting.Grab);
                var grabText = (abstractInteractable as ICanBeGrabbed)?.GetGrabText();
                _currentTexts.Add(grabText);
                if (string.IsNullOrEmpty(firstText))
                {
                    firstText = grabText;
                    grabIcon.CrossFadeAlpha(1f, 0.3f, false);
                }
                else
                    grabIcon.CrossFadeAlpha(0.2f, 0.3f, false);
            }
            else
                grabIcon.gameObject.SetActive(false);
            
            interactText.text = firstText;
        }
        
        public void ShowInspection(GameObject inspectPrefab)
        {
            if (_inspecting)
                return;
            cursorCanvasGroup.DOFade(0f, 1f);
            interactablesCanvasGroup.DOFade(0f, 1f);
            
            _currentInspectText = Instantiate(inspectPrefab, transform);
            _inspecting = true;
        }

        public void Clear()
        {
            if (_inspecting)
            {
                _inspecting = false;
                Destroy(_currentInspectText);
                cursorCanvasGroup.DOFade(1f, 1f);
                interactablesCanvasGroup.DOFade(1f, 1f);
            }
            
            _currentActive = 0;
            _currentSettings.Clear();
            _currentImages.Clear();
            _currentTexts.Clear();
            gameCursor.Clear();
            interactText.text = string.Empty;
            inspectIcon.gameObject.SetActive(false);
            useIcon.gameObject.SetActive(false);
            grabIcon.gameObject.SetActive(false);
        }
    }
}
