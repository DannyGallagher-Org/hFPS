namespace hFPS.Interactable
{
    public class BasicInspectable : AbstractInteractable, ICanInspect
    {
        public string inspectText = "look at";
        public string GetInspectText() => inspectText;
    }
}
