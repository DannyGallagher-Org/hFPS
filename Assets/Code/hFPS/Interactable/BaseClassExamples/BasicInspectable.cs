namespace hFPS.Interactable.BaseClassExamples
{
    public class BasicInspectable : AbstractInteractable, ICanInspect
    {
        public string inspectText = "look at";
        public string GetInspectText() => inspectText;
    }
}
