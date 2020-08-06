using System;
using DG.Tweening;
using UnityEngine;

namespace hFPS.GuiAndHud
{
    public class BasicGuiItem : MonoBehaviour
    {
        private CanvasGroup _group;

        private void Awake()
        {
            _group = GetComponent<CanvasGroup>();
        }

        public void Hide(float duration)
        {
            _group.DOFade(0f, duration);
        }

        public void Show(float duration)
        {
            _group.DOFade(1f, duration);
        }
    }
}
