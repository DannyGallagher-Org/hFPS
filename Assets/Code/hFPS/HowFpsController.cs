using hFPS.GuiAndHud;
using hFPS.Interactable;
using UnityEngine;

namespace hFPS
{
    public class HowFpsController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private HudManager hudManager;
        [SerializeField] private FpsLook fpsLook;
        
        // CurrentTarget
        private AbstractInteractable _currentTarget;
        
        private RaycastHit _result;
        
        private void Update()
        {
            if (Physics.Raycast(new Ray(fpsLook.transform.position, fpsLook.transform.forward), out _result))
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

        private void NoTarget()
        {
            _currentTarget = null;
            hudManager.Clear();
        }
    }
}