namespace hFPS.Interactable
{
    public interface ICanBeUsed
    {
        string GetUseText();
        void DoUse();
        void DoUnuse();
    }
}