using System;
using hFPS.Bindings;
using hFPS.GuiAndHud;
using hFPS.Interactable;
using UnityEngine;

namespace hFPS
{
    public class HowFpsController : MonoBehaviour
    {
        public static HowFpsController Instance;
        
        [Header("Settings")] 
        [SerializeField] private float interactRange = 2f;
        
        [Header("Components")]
        [SerializeField] private HudManager hudManager;
        [SerializeField] private FpsLook fpsLook;
        [SerializeField] private FpsMove fpsMove;
        
        // CurrentTarget
        private AbstractInteractable _currentTarget;
        
        private PlayerActions _playerActions;
        private RaycastHit _result;
        private bool _usingObject;
        private bool _blocked;


        private void Awake()
        {
            Instance = this;
            _playerActions = PlayerActions.CreateWithDefaultBindings();
        }

        private void Update()
        {
            if (_playerActions.InteractLeftHand.WasPressed)
            {
                if(_playerActions.InteractRightHand.IsPressed)
                    BothHands();
                else
                    LeftHand();
            }

            if (_playerActions.InteractRightHand.WasPressed)
            {
                
                if(_playerActions.InteractLeftHand.IsPressed)
                    BothHands();
                else
                    RightHand();
            }

            if (_playerActions.InteractLeftHand.WasReleased)
            {
                if(_currentTarget && (_currentTarget.interactHand == AbstractInteractable.InteractHand.LeftHand || _currentTarget.interactHand == AbstractInteractable.InteractHand.BothHands))
                    DoUnUse();
            }

            if (_playerActions.InteractRightHand.WasReleased)
            {
                if(_currentTarget && (_currentTarget.interactHand == AbstractInteractable.InteractHand.RightHand || _currentTarget.interactHand == AbstractInteractable.InteractHand.BothHands))
                    DoUnUse();
            }
            
            if(_playerActions.InteractScrollUp.WasPressed)
                hudManager.ScrollUp();
            
            if(_playerActions.InteractScrollDown.WasPressed)
                hudManager.ScrollDown();

            if (_usingObject)
                return;
            
            if (Physics.Raycast(new Ray(fpsLook.transform.position, fpsLook.transform.forward), out _result, interactRange))
            {
                var abstractInteractable = _result.transform.GetComponent<AbstractInteractable>();
                if (abstractInteractable && _currentTarget != abstractInteractable)
                {
                    _currentTarget = abstractInteractable;
                    hudManager.SetCursor(_currentTarget);
                }
                else if(!abstractInteractable)
                    NoTarget();
            }
            else if (_currentTarget != null)
                NoTarget();
        }

        private void DoUnUse()
        {
            Debug.Log($"Unuse: {_currentTarget}");
            (_currentTarget as ICanBeUsed)?.DoUnuse();
            _usingObject = false;
        }

        private void BothHands()
        {
            if (_currentTarget && _currentTarget.interactHand == AbstractInteractable.InteractHand.BothHands)
                UseCurrentItemWithSetting();
        }
        
        private void RightHand()
        {
            if (_currentTarget && _currentTarget.interactHand == AbstractInteractable.InteractHand.RightHand)
                UseCurrentItemWithSetting();
        }

        private void LeftHand()
        {
            if (_currentTarget && _currentTarget.interactHand == AbstractInteractable.InteractHand.LeftHand)
                UseCurrentItemWithSetting();
        }

        private void UseCurrentItemWithSetting()
        {
            switch(hudManager.CurrentSetting)
            {
                case HudManager.HudInteractSetting.Inspect:
                    Debug.Log($"Inspect: {_currentTarget}");
                    hudManager.ShowInspection((_currentTarget as ICanInspect).GetInspectPrefab());
                    break;
                case HudManager.HudInteractSetting.Use:
                    Debug.Log($"Use: {_currentTarget}");
                    (_currentTarget as ICanBeUsed).DoUse();
                    _usingObject = true;
                    break;
                case HudManager.HudInteractSetting.Grab:
                    Debug.Log($"Grab: {_currentTarget}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void NoTarget()
        {
            _currentTarget = null;
            hudManager.Clear();
        }

        public void Block(bool blockMove, bool blockLook)
        {
            _blocked = true;
            fpsMove.enabled = !blockMove;
            fpsLook.enabled = !blockLook;
            hudManager.Hide();
        }

        public void UnBlock(bool unblockMove, bool unblockLook)
        {
            _blocked = false;
            fpsMove.enabled = unblockMove;
            fpsLook.enabled = unblockMove;
            hudManager.Show();
        }
    }
}