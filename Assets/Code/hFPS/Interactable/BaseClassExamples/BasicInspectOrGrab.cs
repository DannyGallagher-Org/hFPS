namespace hFPS.Interactable.BaseClassExamples
{
    public class BasicInspectOrGrab : BasicInspectable, ICanBeGrabbed
    {
        public string grabText = "grab";
        
        public string GetGrabText() => grabText;
    }
}