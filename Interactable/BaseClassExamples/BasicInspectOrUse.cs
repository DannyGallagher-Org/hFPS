using System;
using UnityEngine;

namespace hFPS.Interactable.BaseClassExamples
{
    public class BasicInspectOrUse : BasicUseable, ICanInspect
    {
        public string inspectText = "look at";
        public string GetInspectText() => inspectText;
        public GameObject inspectPrefab;
        public GameObject GetInspectPrefab() => inspectPrefab;

        public string inspectID = string.Empty;
        public string GetInspectID() => inspectID;
    }
}