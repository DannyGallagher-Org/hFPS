using UnityEngine;

namespace hFPS.Interactable
{
    public interface ICanInspect
    {
        string GetInspectID();
        string GetInspectText();
        GameObject GetInspectPrefab();
    }
}
